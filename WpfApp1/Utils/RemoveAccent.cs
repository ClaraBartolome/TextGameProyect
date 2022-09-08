using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://github.com/ClaraBartolome/TextGameProyect

namespace StringExtensions
{
    public static class myExtensions
    {
        public static string RemoveAccent(this string texto) =>
        new String(
            texto.Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray()
        )
        .Normalize(NormalizationForm.FormC);
    }

    
}
