/*
 * BIEN: Programación defensiva - fail-fast, valores seguros por defecto, validación
 */

using System.Text.RegularExpressions;

namespace Defensibility.Good
{
    // Excepciones personalizadas para diferentes tipos de errores
    
    /// <summary>
    /// Excepción personalizada para errores de validación
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    /// <summary>
    /// Excepción personalizada para errores de procesamiento de pagos
    /// </summary>
    public class PaymentException : Exception
    {
        public PaymentException(string message, Exception? innerException = null) 
            : base(message, innerException) { }
    }

    /// <summary>
    /// Resultado de pago inmutable
    /// </summary>
    public record PaymentResult(
        string TransactionId,
        decimal Amount,
        string MaskedAccount,
        string Status
    );

    /// <summary>
    /// Validador separado - responsabilidad única
    /// </summary>
    public static class PaymentValidator
    {
        /// <summary>
        /// Fail-fast: Validar monto inmediatamente
        /// </summary>
        public static decimal ValidateAmount(object amount)
        {
            decimal amountDecimal;

            try
            {
                amountDecimal = Convert.ToDecimal(amount);
            }
            catch (Exception)
            {
                throw new ValidationException($"Formato de monto inválido: {amount}");
            }

            if (amountDecimal <= 0)
            {
                throw new ValidationException($"El monto debe ser positivo, se recibió: {amountDecimal}");
            }

            if (amountDecimal > 10000m)
            {
                throw new ValidationException($"El monto excede el límite: {amountDecimal}");
            }

            return amountDecimal;
        }

        /// <summary>
        /// Fail-fast: Validar número de cuenta
        /// </summary>
        public static string ValidateAccount(string accountNumber)
        {
            if (accountNumber == null)
            {
                throw new ValidationException("El número de cuenta no puede ser nulo");
            }

            // Remover guiones y espacios
            string cleanAccount = accountNumber.Replace("-", "").Replace(" ", "");

            if (!Regex.IsMatch(cleanAccount, @"^\d+$"))
            {
                throw new ValidationException("El número de cuenta debe contener solo dígitos");
            }

            if (cleanAccount.Length != 16)
            {
                throw new ValidationException($"El número de cuenta debe tener 16 dígitos, se recibió: {cleanAccount.Length}");
            }

            return cleanAccount;
        }

        /// <summary>
        /// Fail-fast: Validar CVV
        /// </summary>
        public static string ValidateCvv(string cvv)
        {
            if (cvv == null)
            {
                throw new ValidationException("El CVV no puede ser nulo");
            }

            if (!Regex.IsMatch(cvv, @"^\d+$"))
            {
                throw new ValidationException("El CVV debe contener solo dígitos");
            }

            if (cvv.Length != 3 && cvv.Length != 4)
            {
                throw new ValidationException($"El CVV debe tener 3 o 4 dígitos, se recibió: {cvv.Length}");
            }

            return cvv;
        }
    }

    /// <summary>
    /// Procesador de pagos defensivo con valores seguros por defecto
    /// </summary>
    public class PaymentProcessor
    {
        private readonly bool _debugMode;
        private readonly int _maxRetry;
        private readonly int _timeout;
        private int _transactionCount;

        public PaymentProcessor(
            bool debugMode = false,  // Valor seguro por defecto: OFF
            int maxRetry = 3,        // Valor seguro por defecto: límite razonable
            int timeout = 30)        // Valor seguro por defecto: 30 segundos
        {
            _debugMode = debugMode;
            _maxRetry = maxRetry;
            _timeout = timeout;
            _transactionCount = 0;
        }

        /// <summary>
        /// Procesar pago con validación completa y manejo de errores
        /// </summary>
        public PaymentResult ProcessPayment(object amount, string accountNumber, string cvv)
        {
            // FAIL-FAST: Validar todas las entradas inmediatamente
            decimal validatedAmount = PaymentValidator.ValidateAmount(amount);
            string validatedAccount = PaymentValidator.ValidateAccount(accountNumber);
            string validatedCvv = PaymentValidator.ValidateCvv(cvv);

            // LEAST PRIVILEGE: Solo registrar información necesaria, nunca datos sensibles
            if (_debugMode)
            {
                string masked = MaskAccount(validatedAccount);
                Console.WriteLine($"DEBUG: Procesando ${validatedAmount} desde {masked}");
                // Nota: ¡CVV nunca se registra!
            }

            // Procesar pago
            try
            {
                ChargeCard(validatedAmount, validatedAccount, validatedCvv);
            }
            catch (Exception e)
            {
                // FAIL-FAST: No tragarse excepciones
                throw new PaymentException($"Falló el procesamiento del pago: {e.Message}", e);
            }

            // LEAST PRIVILEGE: Almacenar datos mínimos necesarios
            _transactionCount++;
            string transactionId = $"TXN-{_transactionCount:D6}";

            // Crear resultado inmutable
            return new PaymentResult(
                TransactionId: transactionId,
                Amount: validatedAmount,
                MaskedAccount: MaskAccount(validatedAccount),
                Status: "SUCCESS"
            );
        }

        /// <summary>
        /// LEAST PRIVILEGE: Solo mostrar últimos 4 dígitos
        /// </summary>
        private string MaskAccount(string account)
        {
            return $"****-****-****-{account.Substring(account.Length - 4)}";
        }

        private void ChargeCard(decimal amount, string account, string cvv)
        {
            // Simular cobro con protecciones
            // En código real:
            // - Tendría timeout (this._timeout)
            // - Tendría límite de reintentos (this._maxRetry)
            // - Usaría patrón circuit breaker
            // - Nunca almacenaría CVV (cumplimiento PCI)
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // VALOR SEGURO POR DEFECTO: debugMode está OFF por defecto
            var processor = new PaymentProcessor();

            Console.WriteLine("Test 1: Pago válido");
            try
            {
                var result = processor.ProcessPayment(100.50m, "1234-5678-9012-3456", "123");
                Console.WriteLine($"[OK] Éxito: {result}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}\n");
            }

            Console.WriteLine("Test 2: Monto negativo - FALLA RÁPIDO");
            try
            {
                var result = processor.ProcessPayment(-500, "1234-5678-9012-3456", "123");
                Console.WriteLine($"Resultado: {result}\n");
            }
            catch (ValidationException e)
            {
                Console.WriteLine($"[OK] Error capturado inmediatamente: {e.Message}\n");
            }

            Console.WriteLine("Test 3: Número de cuenta inválido - FALLA RÁPIDO");
            try
            {
                var result = processor.ProcessPayment(100, "inválido", "123");
                Console.WriteLine($"Resultado: {result}\n");
            }
            catch (ValidationException e)
            {
                Console.WriteLine($"[OK] Error capturado inmediatamente: {e.Message}\n");
            }

            Console.WriteLine("Test 4: CVV vacío - FALLA RÁPIDO");
            try
            {
                var result = processor.ProcessPayment(100, "1234-5678-9012-3456", "");
                Console.WriteLine($"Resultado: {result}\n");
            }
            catch (ValidationException e)
            {
                Console.WriteLine($"[OK] Error capturado inmediatamente: {e.Message}\n");
            }

            Console.WriteLine("Test 5: Tipo de dato incorrecto - FALLA RÁPIDO");
            try
            {
                var result = processor.ProcessPayment("mucho dinero", "1234-5678-9012-3456", "123");
                Console.WriteLine($"Resultado: {result}\n");
            }
            catch (ValidationException e)
            {
                Console.WriteLine($"[OK] Error capturado inmediatamente: {e.Message}\n");
            }

            Console.WriteLine("Test 6: Monto demasiado grande - FALLA RÁPIDO");
            try
            {
                var result = processor.ProcessPayment(1000000, "1234-5678-9012-3456", "123");
                Console.WriteLine($"Resultado: {result}\n");
            }
            catch (ValidationException e)
            {
                Console.WriteLine($"[OK] Error capturado inmediatamente: {e.Message}\n");
            }

            Console.WriteLine("\n[OK] BENEFICIOS:");
            Console.WriteLine("- Todas las entradas validadas inmediatamente (fail-fast)");
            Console.WriteLine("- Mensajes de error claros");
            Console.WriteLine("- Valores predeterminados seguros (debug OFF, límites razonables, timeouts)");
            Console.WriteLine("- Nunca almacena datos sensibles (CVV)");
            Console.WriteLine("- Solo registra/almacena datos mínimos necesarios (least privilege)");
            Console.WriteLine("- Excepciones propagadas correctamente (no se tragan)");
            Console.WriteLine("- Resultados inmutables previenen manipulación");
        }
    }
}
