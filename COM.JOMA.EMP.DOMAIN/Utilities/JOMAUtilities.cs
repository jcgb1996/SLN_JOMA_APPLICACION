using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Utilities
{
    public static class JOMAUtilities
    {
        public static string ExceptionToString(Exception Value)
        {
            try
            {
                string text = Value.Message;
                if (Value.InnerException != null)
                {
                    text = text + "-" + ExceptionToString(Value.InnerException);
                }

                return QuitarSaltosLinea(text);
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        public static string QuitarSaltosLinea(string texto, string caracterReemplazar = " ")
        {
            if (texto != null)
            {
                if (texto != "")
                {
                    string text = texto.Replace('\n'.ToString(), caracterReemplazar).Replace('\r'.ToString(), caracterReemplazar);
                    text = text.Replace(Environment.NewLine, " ");
                    return Regex.Replace(text, " {2,}", " ");
                }

                return string.Empty;
            }

            return texto;
        }


        public static string CadenaConexion(string dataSource, string initialCatalog, string userId, string password, string cryptoKey, ref string mensaje, long TimeOut = 120L)
        {
            try
            {
                if (string.IsNullOrEmpty(cryptoKey))
                {
                    throw new Exception("cryptoKey is null");
                }

                if (string.IsNullOrEmpty(dataSource))
                {
                    throw new Exception("dataSource is null");
                }

                if (string.IsNullOrEmpty(initialCatalog))
                {
                    throw new Exception("initialCatalog is null");
                }

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("userId is null");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("password is null");
                }

                password = JOMACrypto.DescifrarClave(password, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("password encrypt is invalid");
                }

                return $"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};Connection Timeout={TimeOut};Persist Security Info=True;trustServerCertificate=true;";
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return null;
            }
        }

        public static void SetCultureInfo(string Name)
        {
            CultureInfo cultureInfo = new CultureInfo(Name);
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cultureInfo.NumberFormat.NumberGroupSeparator = ",";
            cultureInfo.NumberFormat.CurrencyGroupSeparator = ",";
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public static string GetFileNameAppSettings()
        {
            var JomaAmbiente = GetJOMAAmbiente();
            return $"Settings/appsettings-{JomaAmbiente.EnumtoString()}.json";
        }
        private static JOMAAmbiente GetJOMAAmbiente()
        {
            var nombreVarJOMAAmbiente = "JOMA_AMBIENTE";
            var strJomaAmbiente = Environment.GetEnvironmentVariable(nombreVarJOMAAmbiente, EnvironmentVariableTarget.Machine);
            if (string.IsNullOrEmpty(strJomaAmbiente))
                strJomaAmbiente = Environment.GetEnvironmentVariable(nombreVarJOMAAmbiente, EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(strJomaAmbiente)) throw new Exception($"Variable de entorno \"{nombreVarJOMAAmbiente}\" no encontrada");
            if (!byte.TryParse(strJomaAmbiente, out byte byteEDOCAmbiente)) throw new Exception($"Valor de la variable de entorno \"{nombreVarJOMAAmbiente}\" no válido: {strJomaAmbiente}");
            if (!Enum.IsDefined(typeof(JOMAAmbiente), byteEDOCAmbiente)) throw new Exception($"Valor de la variable de entorno \"{nombreVarJOMAAmbiente}\" no válido: {strJomaAmbiente}");
            return (JOMAAmbiente)byteEDOCAmbiente;
        }

    }


}
