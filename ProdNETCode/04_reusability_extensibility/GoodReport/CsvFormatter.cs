/*
 * Formateador CSV reutilizable
 */

using System.Text;

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Formateador CSV reutilizable
    /// </summary>
    public class CsvFormatter : IReportFormatter
    {
        public string Format(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
                return string.Empty;

            var report = new StringBuilder();

            // Extraer encabezados del primer elemento
            var headers = data[0].Keys;
            report.AppendLine(string.Join(",", headers));

            // Agregar filas de datos
            foreach (var item in data)
            {
                var values = headers.Select(h => item[h].ToString());
                report.AppendLine(string.Join(",", values));
            }

            return report.ToString();
        }
    }
}
