/*
 * MAL: No portable - ligado a plataforma y entorno específico
 */

namespace Portability.Bad
{
    /// <summary>
    /// No portable - rutas de Windows y suposiciones codificadas directamente
    /// </summary>
    public class DataProcessor
    {
        private readonly string _inputDir;
        private readonly string _outputDir;
        private readonly string _dbHost;
        private readonly int _dbPort;
        private readonly string _apiUrl;

        public DataProcessor()
        {
            // ¡Ruta de Windows codificada directamente!
            _inputDir = @"C:\Users\John\Documents\data";
            _outputDir = @"C:\Users\John\Documents\output";

            // Conexión de base de datos codificada directamente
            _dbHost = "localhost";
            _dbPort = 5432;

            // Endpoint de API codificado directamente
            _apiUrl = "http://localhost:8000/api";
        }

        public string ProcessFile(string filename)
        {
            // Construyendo ruta con separador codificado directamente
            string inputPath = _inputDir + "\\" + filename;
            string outputPath = _outputDir + "\\" + filename.Replace(".txt", ".csv");

            Console.WriteLine($"Procesando: {inputPath}");
            Console.WriteLine($"Salida a: {outputPath}");

            // Simular procesamiento
            Console.WriteLine($"Conectando a base de datos en {_dbHost}:{_dbPort}...");
            Console.WriteLine($"Llamando API en {_apiUrl}...");

            return outputPath;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new DataProcessor();

            try
            {
                processor.ProcessFile("sample.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine("\n[X] PROBLEMAS:");
            Console.WriteLine("- Rutas de Windows codificadas directamente (C:\\ drive)");
            Console.WriteLine("- No funcionará en Linux/Mac");
            Console.WriteLine("- No se puede ejecutar en diferentes entornos (dev/staging/prod)");
            Console.WriteLine("- No se puede probar con diferentes configuraciones");
            Console.WriteLine("- ¡Nombre de usuario 'John' codificado directamente!");
            Console.WriteLine("- Endpoints de base de datos y API codificados directamente");
            Console.WriteLine("- Separador de ruta codificado directamente (\\)");
        }
    }
}
