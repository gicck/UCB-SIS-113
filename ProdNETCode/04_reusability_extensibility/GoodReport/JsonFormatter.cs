/*
 * NUEVO FORMATO - ¡Sin modificar el código existente!
 */

using System.Text.Json;

namespace ReusabilityExtensibility.Good
{
    /// <summary>
    /// Nuevo formateador - agregado sin tocar el código existente!
    /// </summary>
    public class JsonFormatter : IReportFormatter
    {
        public string Format(List<Dictionary<string, object>> data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
        }
    }
}
