/*
 * Generador que trabaja con cualquier formateador - ¡extensible!
 */

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Generador que trabaja con cualquier formateador - ¡extensible!
    /// </summary>
    public class ReportGenerator
    {
        private IReportFormatter _formatter;

        public ReportGenerator(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        /// <summary>
        /// Generar reporte usando el formateador configurado
        /// </summary>
        public string Generate(List<Dictionary<string, object>> data)
        {
            return _formatter.Format(data);
        }

        /// <summary>
        /// Cambiar formateador en tiempo de ejecución
        /// </summary>
        public void SetFormatter(IReportFormatter formatter)
        {
            _formatter = formatter;
        }
    }
}
