/*
 * Formateador HTML reutilizable
 */

using System.Text;

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Formateador HTML reutilizable
    /// </summary>
    public class HtmlFormatter : IReportFormatter
    {
        public string Format(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
                return "<html><body><p>Sin datos</p></body></html>";

            var report = new StringBuilder();
            var headers = data[0].Keys;

            report.AppendLine("<html><body><table>");

            // Encabezados
            report.Append("<tr>");
            foreach (var header in headers)
            {
                report.Append($"<th>{header}</th>");
            }
            report.AppendLine("</tr>");

            // Filas de datos
            foreach (var item in data)
            {
                report.Append("<tr>");
                foreach (var header in headers)
                {
                    report.Append($"<td>{item[header]}</td>");
                }
                report.AppendLine("</tr>");
            }

            report.AppendLine("</table></body></html>");
            return report.ToString();
        }
    }
}
