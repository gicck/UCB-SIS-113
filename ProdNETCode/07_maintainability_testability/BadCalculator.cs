/*
 * MAL: Difícil de mantener y probar
 */

namespace MaintainabilityTestability.Bad
{
    /// <summary>
    /// Calculadora no mantenible con responsabilidades mezcladas
    /// </summary>
    public class Calculator
    {
        private List<string> _history;
        private string _logFile;

        public Calculator()
        {
            _history = new List<string>();
            _logFile = "calculator.log";
        }

        /// <summary>
        /// Método complejo que hace demasiadas cosas
        /// </summary>
        public double? Calculate(string expr)
        {
            // Logging mezclado con lógica de negocio
            DateTime timestamp = DateTime.Now;

            // Lógica de parsing compleja - difícil de probar por separado
            if (expr.Contains("+"))
            {
                var parts = expr.Split('+');
                if (parts.Length != 2)
                {
                    // ¡Escribiendo a archivo - efecto secundario!
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Expresión inválida: {expr}\n");
                    Console.WriteLine("ERROR: Expresión inválida");
                    return null;
                }

                try
                {
                    double a = double.Parse(parts[0].Trim());
                    double b = double.Parse(parts[1].Trim());
                    double result = a + b;

                    // Más efectos secundarios
                    _history.Add($"{expr} = {result}");
                    File.AppendAllText(_logFile, $"{timestamp}: {expr} = {result}\n");

                    return result;
                }
                catch
                {
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Números inválidos en: {expr}\n");
                    return null;
                }
            }
            else if (expr.Contains("-"))
            {
                var parts = expr.Split('-');
                if (parts.Length != 2)
                {
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Expresión inválida: {expr}\n");
                    return null;
                }

                try
                {
                    double a = double.Parse(parts[0].Trim());
                    double b = double.Parse(parts[1].Trim());
                    double result = a - b;

                    _history.Add($"{expr} = {result}");
                    File.AppendAllText(_logFile, $"{timestamp}: {expr} = {result}\n");

                    return result;
                }
                catch
                {
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Números inválidos en: {expr}\n");
                    return null;
                }
            }
            else if (expr.Contains("*"))
            {
                // Más código duplicado...
                var parts = expr.Split('*');
                if (parts.Length != 2)
                {
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Expresión inválida: {expr}\n");
                    return null;
                }

                try
                {
                    double a = double.Parse(parts[0].Trim());
                    double b = double.Parse(parts[1].Trim());
                    double result = a * b;

                    _history.Add($"{expr} = {result}");
                    File.AppendAllText(_logFile, $"{timestamp}: {expr} = {result}\n");

                    return result;
                }
                catch
                {
                    File.AppendAllText(_logFile, $"{timestamp}: ERROR - Números inválidos en: {expr}\n");
                    return null;
                }
            }
            else
            {
                File.AppendAllText(_logFile, $"{timestamp}: ERROR - Operador desconocido en: {expr}\n");
                return null;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var calc = new Calculator();

            Console.WriteLine(calc.Calculate("5 + 3"));
            Console.WriteLine(calc.Calculate("10 - 4"));
            Console.WriteLine(calc.Calculate("6 * 7"));

            Console.WriteLine("\n[X] PROBLEMAS:");
            Console.WriteLine("- No se puede probar la lógica de cálculo sin I/O de archivos");
            Console.WriteLine("- Mucho código duplicado");
            Console.WriteLine("- Lógica compleja anidada de if/elif");
            Console.WriteLine("- Efectos secundarios (escritura de archivos) mezclados con cálculos");
            Console.WriteLine("- Difícil agregar nuevos operadores");
            Console.WriteLine("- No se puede mockear tiempo o sistema de archivos para pruebas");
            Console.WriteLine("- Difícil de entender y modificar");
        }
    }
}
