/*
 * BIEN: Diseño extensible y reutilizable usando el patrón Strategy
 */

namespace ReusabilityExtensibility.Good
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "name", "Producto A" }, { "value", 100 } },
                new Dictionary<string, object> { { "name", "Producto B" }, { "value", 200 } },
                new Dictionary<string, object> { { "name", "Producto C" }, { "value", 150 } }
            };

            // Crear generador con formateador de texto
            var generator = new ReportGenerator(new TextFormatter());

            Console.WriteLine("Formato texto:");
            Console.WriteLine(generator.Generate(data));

            // Cambiar a CSV - ¡no se necesitan cambios en el código!
            Console.WriteLine("\nFormato CSV:");
            generator.SetFormatter(new CsvFormatter());
            Console.WriteLine(generator.Generate(data));

            // Cambiar a HTML
            Console.WriteLine("\nFormato HTML:");
            generator.SetFormatter(new HtmlFormatter());
            Console.WriteLine(generator.Generate(data));

            // NUEVO: Formato JSON - ¡sin modificaciones en las clases existentes!
            Console.WriteLine("\nFormato JSON (¡NUEVO!):");
            generator.SetFormatter(new JsonFormatter());
            Console.WriteLine(generator.Generate(data));

            // NUEVO: Formato Markdown
            Console.WriteLine("\nFormato Markdown (¡NUEVO!):");
            generator.SetFormatter(new MarkdownFormatter());
            Console.WriteLine(generator.Generate(data));

            // Incluso se pueden reutilizar los formateadores directamente en otros contextos
            Console.WriteLine("\n[OK] Reutilizando formateador CSV en otro lugar:");
            var csvFormatter = new CsvFormatter();
            var otherData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "id", 1 }, { "status", "activo" } },
                new Dictionary<string, object> { { "id", 2 }, { "status", "inactivo" } }
            };
            Console.WriteLine(csvFormatter.Format(otherData));

            Console.WriteLine("\n[OK] BENEFICIOS:");
            Console.WriteLine("- Agregar nuevos formatos SIN modificar el código existente");
            Console.WriteLine("- Cada formateador es reutilizable en cualquier contexto");
            Console.WriteLine("- Se pueden intercambiar formateadores en tiempo de ejecución");
            Console.WriteLine("- Fácil de probar cada formateador independientemente");
            Console.WriteLine("- Abierto para extensión, cerrado para modificación");
            Console.WriteLine("- Sin cadenas if/else ni acoplamiento");
        }
    }
}
