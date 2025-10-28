/*
 * Formateador abstracto - define la interfaz
 */

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Interfaz abstracta para formateadores de reportes
    /// </summary>
    public interface IReportFormatter
    {
        /// <summary>
        /// Formatea los datos en un reporte
        /// </summary>
        string Format(List<Dictionary<string, object>> data);
    }
}
