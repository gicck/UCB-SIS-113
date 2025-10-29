/*
 * Programa principal para demostración
 */

using MaintainabilityTestability.Good;

namespace GoodCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calc = new CalculatorWithHistory();

            Console.WriteLine("Calculadora Básica:");
            Console.WriteLine($"5 + 3 = {calc.Calculate("5 + 3").Result}");
            Console.WriteLine($"10 - 4 = {calc.Calculate("10 - 4").Result}");
            Console.WriteLine($"6 * 7 = {calc.Calculate("6 * 7").Result}");
            Console.WriteLine($"20 / 5 = {calc.Calculate("20 / 5").Result}");

            Console.WriteLine("\nHistorial:");
            foreach (var entry in calc.GetHistory())
            {
                Console.WriteLine($"  {entry.Expression} = {entry.Result}");
            }

            Console.WriteLine("\n[OK] BENEFICIOS:");
            Console.WriteLine("- Cada componente tiene una responsabilidad única y clara");
            Console.WriteLine("- Las funciones puras son fáciles de probar");
            Console.WriteLine("- Sin efectos secundarios en la lógica de cálculo");
            Console.WriteLine("- Se puede probar cada componente en aislamiento");
            Console.WriteLine("- Fácil agregar nuevas operaciones");
            Console.WriteLine("- Estructura de código clara y mantenible");
        }
    }
}
