/*
 * NUEVO FORMATO - ¡Markdown!
 */

using System.Text;

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Otro nuevo formateador - ¡aún sin cambios en el código existente!
    /// </summary>
    public class MarkdownFormatter : IReportFormatter
    {
        public string Format(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
                return "Sin datos";

            var report = new StringBuilder();
            var headers = data[0].Keys.ToList();

            // Fila de encabezados
            report.AppendLine("| " + string.Join(" | ", headers) + " |");

            // Separador
            report.AppendLine("| " + string.Join(" | ", headers.Select(_ => "---")) + " |");

            // Filas de datos
            foreach (var item in data)
            {
                var values = headers.Select(h => item[h].ToString());
                report.AppendLine("| " + string.Join(" | ", values) + " |");
            }

            return report.ToString();
        }
    }
}
