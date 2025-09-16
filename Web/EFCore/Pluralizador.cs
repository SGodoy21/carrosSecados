using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Web.EFCore
{
    public class Pluralizador : IPluralizer
    {
        // Diccionario de singularizaciones especiales
        private static readonly Dictionary<string, string> SingularExceptions = new()
        {
            { "Detalles", "Detalle" },
            { "Autores", "Autor" },
            { "Usuarios", "Usuario" },
            { "Lotes", "Lote" },
            { "Series", "Serie" }
        };

        public string Pluralize(string name)
        {
            var words = SplitPascalCase(name);
            for (int i = 0; i < words.Count; i++)
                words[i] = PluralizeWord(words[i]);
            return string.Concat(words);
        }

        public string Singularize(string name)
        {
            var words = SplitPascalCase(name);
            for (int i = 0; i < words.Count; i++)
                words[i] = SingularizeWord(words[i]);
            return string.Concat(words);
        }

        private string PluralizeWord(string word)
        {
            if (word.EndsWith("z"))
                return Regex.Replace(word, "z$", "ces");

            if (word.EndsWith("ción"))
                return Regex.Replace(word, "cion$", "ciones");

            if (!word.EndsWith("s"))
                return word + "s";

            return word;
        }

        private string SingularizeWord(string word)
        {
            // Excepciones definidas manualmente
            if (SingularExceptions.TryGetValue(word, out var singular))
                return singular;

            if (Regex.IsMatch(word, @"[cC]es$"))
                return Regex.Replace(word, "ces$", "z");

            if (Regex.IsMatch(word, @"[cC]iones$"))
                return Regex.Replace(word, "ciones$", "cion");

            if (word.EndsWith("es") && word.Length > 4)
                return word.Substring(0, word.Length - 2);

            if (word.EndsWith("s") && word.Length > 3)
                return word.Substring(0, word.Length - 1);

            return word;
        }

        private List<string> SplitPascalCase(string input)
        {
            // Divide PascalCase en palabras: "TomosDetalles" -> ["Tomos", "Detalles"]
            return Regex.Split(input, @"(?<!^)(?=[A-ZÁÉÍÓÚÑ])").ToList();
        }
    }
}
