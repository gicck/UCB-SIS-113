/*
 * Formateador de texto reutilizable
 */

using System.Text;

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Formateador de texto reutilizable
    /// </summary>
    public class TextFormatter : IReportFormatter
    {
        public string Format(List<Dictionary<string, object>> data)
        {
            var report = new StringBuilder();
            report.AppendLine("===== REPORTE =====");
            foreach (var item in data)
            {
                report.AppendLine($"- {item["name"]}: ${item["value"]}");
            }
            report.AppendLine("==================");
            return report.ToString();
        }
    }
}
