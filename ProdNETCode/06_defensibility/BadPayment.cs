/*
 * MAL: No defensivo - fallos silenciosos, valores predeterminados inseguros, sin validación
 */

namespace Defensibility.Bad
{
    /// <summary>
    /// Procesador de pagos inseguro - muchos problemas de seguridad y confiabilidad
    /// </summary>
    public class PaymentProcessor
    {
        private List<Dictionary<string, object>> _transactions;
        private bool _debugMode;
        private int _maxRetry;
        private int? _timeout;

        public PaymentProcessor()
        {
            _transactions = new List<Dictionary<string, object>>();
            _debugMode = true;  // ¡Valor predeterminado inseguro!
            _maxRetry = 100;  // ¡Límite excesivo por defecto!
            _timeout = null;  // ¡Sin timeout - puede colgarse para siempre!
        }

        /// <summary>
        /// Procesar pago - sin validación, fallos silenciosos
        /// </summary>
        public object? ProcessPayment(object amount, object accountNumber, object? cvv = null)
        {
            // ¡Sin validación - acepta cualquier entrada!
            // ¿Montos negativos? ¡Claro!
            // ¿Strings en lugar de números? ¡Claro!
            // ¿Campos requeridos faltantes? ¡Claro!

            if (_debugMode)
            {
                // Registrando datos sensibles - ¡PROBLEMA DE SEGURIDAD!
                Console.WriteLine($"DEBUG: Procesando ${amount} desde {accountNumber}, CVV: {cvv}");
            }

            // Simular procesamiento de pago
            try
            {
                // Sin validación de entrada
                var result = ChargeCard(amount, accountNumber, cvv);

                // Fallo silencioso - solo retorna null
                if (!result)
                {
                    return null;
                }

                _transactions.Add(new Dictionary<string, object>
                {
                    { "amount", amount },
                    { "account", accountNumber },  // ¡Almacenando número de cuenta completo!
                    { "cvv", cvv ?? "" }  // ¡Almacenando CVV - PROBLEMA GRAVE DE SEGURIDAD!
                });

                return result;
            }
            catch (Exception e)
            {
                // ¡Tragándose excepciones - fallo silencioso!
                if (_debugMode)
                {
                    Console.WriteLine($"Ocurrió un error: {e.Message}");
                }
                return null;
            }
        }

        private bool ChargeCard(object amount, object account, object? cvv)
        {
            // Simular cobro - sin protecciones
            // En código real, llamaría a API de pago
            // Sin límites de reintentos, sin timeouts, sin circuit breaker
            return true;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new PaymentProcessor();

            Console.WriteLine("Test 1: Pago válido");
            var result = processor.ProcessPayment(100.50, "1234-5678-9012-3456", "123");
            Console.WriteLine($"Resultado: {result}\n");

            Console.WriteLine("Test 2: Monto negativo - ¡debería fallar pero no lo hace!");
            result = processor.ProcessPayment(-500, "1234-5678-9012-3456", "123");
            Console.WriteLine($"Resultado: {result} [X] ¡Aceptó monto negativo!\n");

            Console.WriteLine("Test 3: Número de cuenta inválido - ¡sin validación!");
            result = processor.ProcessPayment(100, "inválido", "123");
            Console.WriteLine($"Resultado: {result} [X] ¡Aceptó cuenta inválida!\n");

            Console.WriteLine("Test 4: CVV faltante - ¡sin validación!");
            result = processor.ProcessPayment(100, "1234-5678-9012-3456", null);
            Console.WriteLine($"Resultado: {result} [X] ¡Aceptó CVV faltante!\n");

            Console.WriteLine("Test 5: Tipo de dato incorrecto - ¡sin validación!");
            result = processor.ProcessPayment("mucho dinero", 12345, new List<int> { 1, 2, 3 });
            Console.WriteLine($"Resultado: {result} [X] ¡Aceptó tipos incorrectos!\n");

            Console.WriteLine("[X] PROBLEMAS:");
            Console.WriteLine("- Sin validación de entrada");
            Console.WriteLine("- Fallos silenciosos (retorna null en lugar de lanzar errores)");
            Console.WriteLine("- Almacena datos sensibles (CVV) - ¡violación de cumplimiento PCI!");
            Console.WriteLine("- Modo debug ACTIVADO por defecto - filtra información sensible");
            Console.WriteLine("- Sin timeout - puede colgarse para siempre");
            Console.WriteLine("- Límite de reintentos excesivo (100)");
            Console.WriteLine("- Errores descubiertos tarde o nunca");
        }
    }
}
