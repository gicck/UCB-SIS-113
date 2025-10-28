/*
 * MAL: Código rígido, no reutilizable ni extensible
 */

using System.Text;

namespace ReusabilityExtensibility.Bad
{
    /// <summary>
    /// Generador de reportes monolítico - difícil de extender
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Generar reporte - ¡debe modificar este método para agregar nuevos formatos!
        /// </summary>
        public string GenerateReport(List<Dictionary<string, object>> data, string formatType)
        {
            if (formatType == "text")
            {
                // Lógica de formato texto
                var report = new StringBuilder();
                report.AppendLine("===== REPORTE =====");
                foreach (var item in data)
                {
                    report.AppendLine($"- {item["name"]}: ${item["value"]}");
                }
                report.AppendLine("==================");
                return report.ToString();
            }
            else if (formatType == "csv")
            {
                // Lógica de formato CSV
                var report = new StringBuilder();
                report.AppendLine("name,value");
                foreach (var item in data)
                {
                    report.AppendLine($"{item["name"]},{item["value"]}");
                }
                return report.ToString();
            }
            else if (formatType == "html")
            {
                // Lógica de formato HTML
                var report = new StringBuilder();
                report.AppendLine("<html><body><table>");
                report.AppendLine("<tr><th>Name</th><th>Value</th></tr>");
                foreach (var item in data)
                {
                    report.AppendLine($"<tr><td>{item["name"]}</td><td>${item["value"]}</td></tr>");
                }
                report.AppendLine("</table></body></html>");
                return report.ToString();
            }
            else
            {
                throw new ArgumentException($"Formato desconocido: {formatType}");
            }

            // ¿Quiere formato JSON? ¡Debe modificar este método!
            // ¿Quiere formato XML? ¡Debe modificar este método!
            // ¿Quiere formato Markdown? ¡Debe modificar este método!
        }
    }

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

            var generator = new ReportGenerator();

            Console.WriteLine("Formato texto:");
            Console.WriteLine(generator.GenerateReport(data, "text"));

            Console.WriteLine("\nFormato CSV:");
            Console.WriteLine(generator.GenerateReport(data, "csv"));

            Console.WriteLine("\nFormato HTML:");
            Console.WriteLine(generator.GenerateReport(data, "html"));

            Console.WriteLine("\n[X] PROBLEMAS:");
            Console.WriteLine("- Debe modificar ReportGenerator para agregar nuevos formatos");
            Console.WriteLine("- Viola el Principio de Abierto/Cerrado");
            Console.WriteLine("- No se puede reutilizar la lógica de formato en otro lugar");
            Console.WriteLine("- No se pueden combinar o componer formateadores");
            Console.WriteLine("- Cadena creciente de if/else");
            Console.WriteLine("- Todos los formatos acoplados en una clase");
        }
    }
}
