using COM.JOMA.EMP.DOMAIN.Attributes;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;

namespace COM.JOMA.EMP.DOMAIN.Extensions
{
    public static class DomainExtensions
    {


        public static T? Get<T>(this GlobalDictionaryDto obj, string key)
        {
            try
            {
                if (obj.Items.ContainsKey(key))
                {
                    return (T)obj.Items[key];
                }
                return default;
            }
            catch (Exception) { return default; }
        }
        public static void Add(this GlobalDictionaryDto obj, string key, object value)
        {
            try
            {
                if (obj.Items.ContainsKey(key))
                    obj.Items.Remove(key);

                obj.Items.Add(key, value);
            }
            catch (Exception) { }
        }
        public static string GetCodigo(this JOMAComponente obj)
        {
            try
            {
                var det = obj.GetAttribute<JomaDetComponenteAttribute>();
                return det.Codigo;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetNombre(this JOMAComponente obj)
        {
            try
            {
                var det = obj.GetAttribute<JomaDetComponenteAttribute>();
                return det.Nombre ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GenerarMensajeErrorGenerico(this GlobalDictionaryDto obj, string? CodigoSeguimiento, string? NombreLog = null)
        {
            string? NombreArchivoLog = null;
            if (!string.IsNullOrEmpty(NombreLog))
                NombreArchivoLog = NombreLog;
            else
                NombreArchivoLog = obj.Get<string>(DomainConstants.JOMA_KEY_GLOBAL_DICT_NOMBRELOG);
            if (string.IsNullOrEmpty(NombreArchivoLog))
            {
                return string.Format(DomainConstants.JOMA_FAULTSTRING, CodigoSeguimiento);
            }
            else
            {
                return string.Format(DomainConstants.JOMA_FAULTSTRING, $"{NombreArchivoLog}-{CodigoSeguimiento}");
            }
        }


    }
}
