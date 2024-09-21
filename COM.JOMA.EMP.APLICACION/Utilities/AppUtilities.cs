using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
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

        public static bool ValidarCedulaEcuatoriana(string Cedula)
        {
            if (string.IsNullOrWhiteSpace(Cedula) || Cedula.Length != 10)
                return false;

            if (!Cedula.All(char.IsDigit))
                return false;

            int provincia = int.Parse(Cedula.Substring(0, 2));
            int tercerDigito = int.Parse(Cedula.Substring(2, 1));

            if ((provincia < 1 || provincia > 24) && provincia != 30)
                return false;

            if (tercerDigito < 0 || tercerDigito > 5)
                return false;

            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < 9; i++)
            {
                int digito = int.Parse(Cedula.Substring(i, 1));
                int producto = digito * coeficientes[i];
                if (producto >= 10)
                    producto -= 9;
                suma += producto;
            }

            int ultimoDigitoCalculado = suma % 10 != 0 ? 10 - (suma % 10) : 0;
            int ultimoDigitoCedula = int.Parse(Cedula.Substring(9, 1));

            return ultimoDigitoCalculado == ultimoDigitoCedula;
        }

        public static string ReemplezarTildes(string text)
        {
            Dictionary<char, char> DiacriticsMap = new Dictionary<char, char>
            {
                { 'á', 'a' }, { 'é', 'e' }, { 'í', 'i' }, { 'ó', 'o' }, { 'ú', 'u' },
                { 'Á', 'A' }, { 'É', 'E' }, { 'Í', 'I' }, { 'Ó', 'O' }, { 'Ú', 'U' },
                { 'ñ', 'n' }, { 'Ñ', 'N' }
            };


            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                result[i] = DiacriticsMap.ContainsKey(c) ? DiacriticsMap[c] : c;
            }
            return new string(result);
        }

        public static string GenerarContrasenaAleatoria(byte longitudContrasenia = 10)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890._-*$#%&?!";
            int longitud = caracteres.Length;
            char letra;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

        public static bool EsMenor(DateTime FechaNacimiento)
        {
            int DifAnios = DateTime.Today.Year - FechaNacimiento.Year;
            if (FechaNacimiento > DateTime.Today.AddYears(-DifAnios)) DifAnios--;
            return DifAnios < DomainConstants.JOMA_MENOR_EDAD;
        }
    }
}
