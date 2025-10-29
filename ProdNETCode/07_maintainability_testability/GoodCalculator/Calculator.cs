/*
 * BIEN: Calculadora mantenible y testeable
 */

namespace MaintainabilityTestability.Good
{
    /// <summary>
    /// Estructura de datos clara para resultados
    /// </summary>
    public record CalculationResult(
        string Expression,
        double Result,
        DateTime Timestamp
    );

    /// <summary>
    /// Responsabilidad única: Parsear expresiones
    /// </summary>
    public static class OperationParser
    {
        private static readonly char[] Operators = { '+', '-', '*', '/' };

        /// <summary>
        /// Parsear expresión en (operando1, operador, operando2)
        /// </summary>
        public static (double, char, double) Parse(string expr)
        {
            expr = expr.Trim();

            foreach (char op in Operators)
            {
                if (expr.Contains(op))
                {
                    var parts = expr.Split(op);
                    if (parts.Length != 2)
                    {
                        throw new ArgumentException($"Expresión inválida: {expr}");
                    }

                    try
                    {
                        double a = double.Parse(parts[0].Trim());
                        double b = double.Parse(parts[1].Trim());
                        return (a, op, b);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException($"Números inválidos en expresión: {expr}");
                    }
                }
            }

            throw new ArgumentException($"No se encontró operador válido en: {expr}");
        }
    }

    /// <summary>
    /// Responsabilidad única: Operaciones matemáticas (funciones puras)
    /// </summary>
    public static class Operations
    {
        /// <summary>
        /// Función pura - ¡fácil de probar!
        /// </summary>
        public static double Add(double a, double b) => a + b;

        /// <summary>
        /// Función pura - ¡fácil de probar!
        /// </summary>
        public static double Subtract(double a, double b) => a - b;

        /// <summary>
        /// Función pura - ¡fácil de probar!
        /// </summary>
        public static double Multiply(double a, double b) => a * b;

        /// <summary>
        /// Función pura - ¡fácil de probar!
        /// </summary>
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("División por cero");
            }
            return a / b;
        }

        /// <summary>
        /// Obtener función de operación por símbolo de operador
        /// </summary>
        public static Func<double, double, double> GetOperation(char operatorSymbol)
        {
            return operatorSymbol switch
            {
                '+' => Add,
                '-' => Subtract,
                '*' => Multiply,
                '/' => Divide,
                _ => throw new ArgumentException($"Operador desconocido: {operatorSymbol}")
            };
        }
    }

    /// <summary>
    /// Calculadora limpia - delega a componentes especializados
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Calcular expresión - simple y testeable
        /// </summary>
        public double Calculate(string expr)
        {
            // Parsear
            var (a, operatorSymbol, b) = OperationParser.Parse(expr);

            // Obtener operación
            var operation = Operations.GetOperation(operatorSymbol);

            // Calcular (función pura - ¡sin efectos secundarios!)
            double result = operation(a, b);

            return result;
        }
    }

    /// <summary>
    /// Calculadora con historial - testeable con inyección de dependencias
    /// </summary>
    public class CalculatorWithHistory
    {
        private readonly Calculator _calculator;
        private readonly List<CalculationResult> _history;

        public CalculatorWithHistory(Calculator? calculator = null)
        {
            _calculator = calculator ?? new Calculator();
            _history = new List<CalculationResult>();
        }

        /// <summary>
        /// Calcular y registrar en historial
        /// </summary>
        public CalculationResult Calculate(string expr)
        {
            double result = _calculator.Calculate(expr);

            var calcResult = new CalculationResult(
                Expression: expr,
                Result: result,
                Timestamp: DateTime.Now
            );

            _history.Add(calcResult);
            return calcResult;
        }

        /// <summary>
        /// Obtener historial de cálculos
        /// </summary>
        public List<CalculationResult> GetHistory()
        {
            return new List<CalculationResult>(_history);
        }
    }
}
