/*
MALO: Sobre-ingenierado, repetitivo, con características innecesarias
Viola KISS, DRY y YAGNI
*/
using System;

namespace StringUtilsDemo
{
    /// <summary>
    /// YAGNI: ¿Realmente necesitamos un enum para esto?
    /// </summary>
    public enum StringCase
    {
        Upper,
        Lower,
        Title,
        Capitalize
    }

    /// <summary>
    /// Violación KISS: Abstracción innecesaria para operaciones simples
    /// </summary>
    public abstract class StringTransformerInterface
    {
        public abstract string Transform(string text);
    }

    /// <summary>
    /// Violación KISS: Clase para una operación de una línea
    /// </summary>
    public class UpperCaseTransformer : StringTransformerInterface
    {
        public override string Transform(string text)
        {
            return text.ToUpper();
        }
    }

    /// <summary>
    /// Violación KISS: Otra clase para una operación de una línea
    /// </summary>
    public class LowerCaseTransformer : StringTransformerInterface
    {
        public override string Transform(string text)
        {
            return text.ToLower();
        }
    }

    /// <summary>
    /// Violación KISS: Otra clase más
    /// </summary>
    public class TitleCaseTransformer : StringTransformerInterface
    {
        public override string Transform(string text)
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
    }

    /// <summary>
    /// Violación KISS: ¡Fábrica para operaciones simples de strings!
    /// </summary>
    public class TransformerFactory
    {
        public static StringTransformerInterface CreateTransformer(
            StringCase caseType)
        {
            switch (caseType)
            {
                case StringCase.Upper:
                    return new UpperCaseTransformer();
                case StringCase.Lower:
                    return new LowerCaseTransformer();
                case StringCase.Title:
                    return new TitleCaseTransformer();
                default:
                    throw new ArgumentException(
                        $"Tipo de caso desconocido: {caseType}");
            }
        }
    }

    /// <summary>
    /// Código repetitivo y características sobre-ingenieradas
    /// </summary>
    public class StringProcessor
    {
        private readonly TransformerFactory _factory;

        public StringProcessor()
        {
            _factory = new TransformerFactory();
        }

        // Violación DRY: Métodos de saludo repetitivos
        
        /// <summary>
        /// Lógica repetida
        /// </summary>
        public string GreetUserMorning(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "¡Hola, Invitado! ¡Buenos días!";
            
            if (name.Length > 20)
                name = name.Substring(0, 20) + "...";
            
            return $"¡Hola, {name}! ¡Buenos días!";
        }

        /// <summary>
        /// Misma lógica, saludo diferente - ¡Violación DRY!
        /// </summary>
        public string GreetUserAfternoon(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "¡Hola, Invitado! ¡Buenas tardes!";
            
            if (name.Length > 20)
                name = name.Substring(0, 20) + "...";
            
            return $"¡Hola, {name}! ¡Buenas tardes!";
        }

        /// <summary>
        /// ¡Más repetición!
        /// </summary>
        public string GreetUserEvening(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "¡Hola, Invitado! ¡Buenas noches!";
            
            if (name.Length > 20)
                name = name.Substring(0, 20) + "...";
            
            return $"¡Hola, {name}! ¡Buenas noches!";
        }

        // Violación YAGNI: Características que aún no se necesitan

        /// <summary>
        /// YAGNI: No se necesita ahora, puede que nunca se necesite
        /// </summary>
        public string FutureFeatureTranslate(string text, string language)
        {
            // Marcador de posición para característica futura de traducción
            return text;
        }

        /// <summary>
        /// YAGNI: Construyendo para un futuro hipotético
        /// </summary>
        public string FutureFeatureSentimentAnalysis(string text)
        {
            // Marcador de posición para análisis de sentimiento
            return "neutral";
        }

        /// <summary>
        /// YAGNI: Otra característica no utilizada
        /// </summary>
        public void FutureFeatureTextToSpeech(string text)
        {
            // Sin implementación
        }

        /// <summary>
        /// Violación KISS: Innecesariamente complejo
        /// </summary>
        public string ReverseStringComplex(string text)
        {
            var result = new System.Collections.Generic.List<char>();
            int index = text.Length - 1;
            
            while (index >= 0)
            {
                char ch = text[index];
                result.Add(ch);
                index = index - 1;
            }
            
            return new string(result.ToArray());
        }

        public StringTransformerInterface GetTransformer(StringCase caseType)
        {
            return TransformerFactory.CreateTransformer(caseType);
        }
    }

    public class BadStringUtilsProgram
    {
        public static void Main(string[] args)
        {
            var processor = new StringProcessor();

            // Usando el transformador sobre-ingenierado
            var transformer = processor.GetTransformer(StringCase.Upper);
            Console.WriteLine(transformer.Transform("hola"));

            // Usando métodos de saludo repetitivos
            Console.WriteLine(processor.GreetUserMorning("Alicia"));
            Console.WriteLine(processor.GreetUserAfternoon("Roberto"));
            Console.WriteLine(processor.GreetUserEvening("Carlos"));

            // Inversión de string compleja
            Console.WriteLine(processor.ReverseStringComplex("hola"));

            Console.WriteLine("\n[X] PROBLEMAS:");
            Console.WriteLine(
                "- KISS: Sobre-ingenierado con clases y fábricas innecesarias");
            Console.WriteLine(
                "- DRY: Lógica de saludo repetida en 3 métodos");
            Console.WriteLine(
                "- YAGNI: Características 'futuras' sin usar que saturan el código");
            Console.WriteLine(
                "- Operaciones simples envueltas en abstracciones complejas");
            Console.WriteLine("- Difícil de entender y mantener");
            Console.WriteLine("- Más código para probar y depurar");
        }
    }
}