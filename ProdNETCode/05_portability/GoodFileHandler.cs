/*
 * BIEN: Portable a través de plataformas y entornos
 */

using Microsoft.Extensions.Configuration;

namespace Portability.Good
{
    /// <summary>
    /// Abstracción de configuración - puede cargar desde diferentes fuentes
    /// </summary>
    public class AppConfig
    {
        public string InputDir { get; set; } = "./data";
        public string OutputDir { get; set; } = "./output";
        public string DbHost { get; set; } = "localhost";
        public int DbPort { get; set; } = 5432;
        public string ApiUrl { get; set; } = "http://localhost:8000/api";

        /// <summary>
        /// Carga la configuración desde appsettings.json y variables de entorno
        /// </summary>
        public static AppConfig Load()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var config = new AppConfig();
            configuration.Bind(config);

            // Asegurar que los directorios existan
            Directory.CreateDirectory(config.InputDir);
            Directory.CreateDirectory(config.OutputDir);

            return config;
        }
    }

    /// <summary>
    /// Portable - funciona en cualquier plataforma
    /// </summary>
    public class DataProcessor
    {
        private readonly AppConfig _config;

        public DataProcessor(AppConfig? config = null)
        {
            _config = config ?? AppConfig.Load();
        }

        public string ProcessFile(string filename)
        {
            // Manejo de rutas multiplataforma con Path.Combine
            string inputPath = Path.Combine(_config.InputDir, filename);

            // Manipulación de rutas independiente de la plataforma
            string outputFilename = Path.GetFileNameWithoutExtension(filename) + ".csv";
            string outputPath = Path.Combine(_config.OutputDir, outputFilename);

            Console.WriteLine($"Procesando: {inputPath}");
            Console.WriteLine($"Salida a: {outputPath}");

            // Simular procesamiento
            Console.WriteLine($"Conectando a base de datos en {_config.DbHost}:{_config.DbPort}...");
            Console.WriteLine($"Llamando API en {_config.ApiUrl}...");

            return outputPath;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            DemonstratePortability();
        }

        static void DemonstratePortability()
        {
            Console.WriteLine("=== Escenario 1: Configuración predeterminada ===");
            var processor = new DataProcessor();
            processor.ProcessFile("sample.txt");

            Console.WriteLine("\n=== Escenario 2: Entorno personalizado (ej: Producción) ===");
            // Simular configuración de producción
            var prodConfig = new AppConfig
            {
                InputDir = "/var/app/data",
                OutputDir = "/var/app/output",
                DbHost = "prod-db.ejemplo.com",
                DbPort = 5432,
                ApiUrl = "https://api.ejemplo.com/v1"
            };

            var prodProcessor = new DataProcessor(prodConfig);
            prodProcessor.ProcessFile("sample.txt");

            Console.WriteLine("\n=== Escenario 3: Rutas independientes de plataforma ===");
            Console.WriteLine($"Sistema operativo actual: {Environment.OSVersion.Platform}");
            Console.WriteLine($"Separador de ruta: {Path.DirectorySeparatorChar}");

            // Path.Combine maneja esto automáticamente!
            string demoPath = Path.Combine("data", "subcarpeta", "archivo.txt");
            Console.WriteLine($"Ruta multiplataforma: {demoPath}");
            Console.WriteLine($"Ruta absoluta: {Path.GetFullPath(demoPath)}");

            // ¡Funciona igual en Windows, Linux, Mac!

            Console.WriteLine("\n[OK] BENEFICIOS:");
            Console.WriteLine("- Funciona en Windows, Linux y Mac sin cambios");
            Console.WriteLine("- Configuración específica del entorno a través de archivos JSON");
            Console.WriteLine("- Sin rutas o servidores codificados directamente");
            Console.WriteLine("- Fácil de probar con diferentes configuraciones");
            Console.WriteLine("- Path.Combine proporciona manejo de rutas multiplataforma");
            Console.WriteLine("- Se puede desplegar en cualquier entorno (dev/staging/prod)");
        }
    }
}
