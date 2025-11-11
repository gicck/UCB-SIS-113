// BUENO: Simple, DRY y solo lo necesario
using System;
using System.Linq;

namespace StringUtilsDemo
{
    public static class StringUtils
    {
        /// <summary>
        /// KISS: Función simple para conversión de mayúsculas/minúsculas
        /// Sin clases o fábricas innecesarias
        /// </summary>
        public static string ChangeCase(string text, string caseType)
        {
            caseType = caseType.ToLower();

            switch (caseType)
            {
                case "upper":
                    return text.ToUpper();
                case "lower":
                    return text.ToLower();
                case "title":
                    return ToTitleCase(text);
                default:
                    throw new ArgumentException(
                        $"Tipo de caso desconocido: {caseType}");
            }
        }

        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + 
                               words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }

        /// <summary>
        /// DRY: Función única con parámetro en lugar de 3 funciones separadas
        /// Lógica escrita una vez, reutilizada para todos los momentos del día
        /// </summary>
        public static string GreetUser(string name, string timeOfDay = "día")
        {
            // Manejar nombre vacío
            if (string.IsNullOrEmpty(name))
                name = "Invitado";

            // Truncar nombres largos
            if (name.Length > 20)
                name = name.Substring(0, 20) + "...";

            return $"¡Hola, {name}! ¡Buen {timeOfDay}!";
        }

        /// <summary>
        /// KISS: Usa el método simple de C# con LINQ
        /// No necesita bucles complejos
        /// </summary>
        public static string ReverseString(string text)
        {
            return new string(text.Reverse().ToArray());
        }

        /// <summary>
        /// KISS: Función simple de un solo propósito
        /// Solo agregada porque realmente se necesita
        /// </summary>
        public static string TruncateText(
            string text, 
            int maxLength, 
            string suffix = "...")
        {
            if (text.Length <= maxLength)
                return text;
            
            return text.Substring(0, maxLength) + suffix;
        }
    }

    // YAGNI: ¡No hay "características futuras" sin usar aquí!
    // Solo implementar lo que realmente se necesita ahora mismo.
    // Cuando se necesite traducción, agrégala en ese momento.

    public class GoodStringUtilsProgram
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Conversión de Mayúsculas/Minúsculas (KISS) ===");
            string text = "hola mundo";
            Console.WriteLine($"Original: {text}");
            Console.WriteLine($"Mayúsculas: {StringUtils.ChangeCase(text, "upper")}");
            Console.WriteLine(
                $"Minúsculas: {StringUtils.ChangeCase(text.ToUpper(), "lower")}");
            Console.WriteLine($"Título: {StringUtils.ChangeCase(text, "title")}");

            Console.WriteLine("\n=== Saludos (DRY) ===");
            Console.WriteLine(StringUtils.GreetUser("Alicia", "día"));
            Console.WriteLine(StringUtils.GreetUser("Roberto", "tarde"));
            Console.WriteLine(StringUtils.GreetUser("Carlos", "noche"));
            Console.WriteLine(StringUtils.GreetUser("", "día")); // Nombre vacío
            Console.WriteLine(
                StringUtils.GreetUser(
                    "NombreMuyLargoQueSuperaLosVeinteCaracteres", "día"));

            Console.WriteLine("\n=== Inversión de String (KISS) ===");
            Console.WriteLine(
                $"Invertir 'hola': {StringUtils.ReverseString("hola")}");
            Console.WriteLine(
                $"Invertir 'CSharp': {StringUtils.ReverseString("CSharp")}");

            Console.WriteLine("\n=== Truncar (YAGNI) ===");
            string longText = "Este es un texto muy largo que necesita truncamiento";
            Console.WriteLine(StringUtils.TruncateText(longText, 20));
            Console.WriteLine(StringUtils.TruncateText(longText, 30, "..."));

            Console.WriteLine("\n[✓] BENEFICIOS:");
            Console.WriteLine("- KISS: Código simple y directo");
            Console.WriteLine("- DRY: Sin lógica repetida");
            Console.WriteLine("- YAGNI: Solo lo necesario, nada extra");
            Console.WriteLine("- Fácil de entender y modificar");
            Console.WriteLine("- Menos código para mantener y probar");
            Console.WriteLine("- Aprovecha las características integradas de C#");

            Console.WriteLine("\n[✓] COMPARACIÓN:");
            Console.WriteLine(
                "Ejemplo malo: ~100+ líneas con clases, fábricas, enums");
            Console.WriteLine(
                "Ejemplo bueno: ~80 líneas con funciones simples");
            Console.WriteLine("¡Misma funcionalidad, mucho más simple!");
        }
    }
}