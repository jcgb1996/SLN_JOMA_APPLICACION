using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Utilities
{
    public static class AppUtilities
    {
        public static long DBNullToLong(object? Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                    return 0;
                if (Value == null)
                    return 0;

                return Convert.ToInt64(Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string RemoveDiacritics(string strNombre)
        {
            if (string.IsNullOrEmpty(strNombre))
                return strNombre;

            var normalizedString = strNombre.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static string QuitarSaltosLinea(string texto, string caracterReemplazar = " ")
        {
            if (string.IsNullOrEmpty(texto))
                return texto;
            // Reemplaza los saltos de línea con el caracter especificado.
            string resultado = texto.Replace("\r\n", caracterReemplazar) // Para Windows
                                    .Replace("\n", caracterReemplazar)   // Para Unix/Linux
                                    .Replace("\r", caracterReemplazar);  // Para sistemas más antiguos

            // Reemplaza puntos y comas con comas y reduce espacios múltiples a uno solo.
            resultado = resultado.Replace(";", ",")
                                 .Replace("  ", " ");
            // Reduce cualquier espacio múltiple a un solo espacio.
            resultado = Regex.Replace(resultado, @"\s+", " ");
            return resultado;
        }
    }
}
