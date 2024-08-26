using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using COM.JOMA.EMP.DOMAIN.Utilities;

namespace COM.JOMA.EMP.DOMAIN.Tools
{
    public static class JOMAConversions
    {
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static DateTime DBNullToDate(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return DateTime.Parse("0001-01-01");
            }

            if (Value == null)
            {
                return DateTime.Parse("0001-01-01");
            }

            return Convert.ToDateTime(Value);
        }

        public static string DBNullToString(object Value)
        {
            if (Convert.IsDBNull(Value) || Value == null)
            {
                return string.Empty;
            }

            // El uso de ?. asegura que ToString() solo se llama si Value no es null
            return Value?.ToString() ?? string.Empty;
        }

        public static object DBNullToNothing(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return null;
            }

            if (Value == null)
            {
                return null;
            }

            return Value;
        }

        public static string DBNullDateToString(object Value, string format)
        {
            if (Convert.IsDBNull(Value))
            {
                return string.Empty;
            }

            if (Value == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(Value).ToString(format);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static int DBNullToInt32(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }

            if (Value == null)
            {
                return 0;
            }

            if (IsNumeric(Value))
            {
                return Convert.ToInt32(Value);
            }

            return 0;
        }

        public static short DBNullToInt16(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }

            if (Value == null)
            {
                return 0;
            }

            if (IsNumeric(Value))
            {
                return Convert.ToInt16(Value);
            }

            return 0;
        }

        public static byte DBNullToByte(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0;
            }

            if (Value == null)
            {
                return 0;
            }

            if (IsNumeric(Value))
            {
                return Convert.ToByte(Value);
            }

            return 0;
        }

        public static string DecimalToFixedString(decimal Value)
        {
            string text = $"{Value:0.00}";
            return text.Replace(",", string.Empty).Replace(".", string.Empty);
        }

        public static decimal FixedStringToDecimal(string Value)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return 0m;
            }

            int length = Value.Length;
            string value = "0";
            string value2;
            if (length > 2)
            {
                value = Value.Substring(0, length - 2);
                value2 = Value.Substring(length - 2);
            }
            else
            {
                value2 = Value;
            }

            decimal num = Convert.ToDecimal(value);
            decimal num2 = Convert.ToDecimal(value2);
            num2 *= 0.01m;
            return num + num2;
        }

        public static bool DBNullToBool(object Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                {
                    return false;
                }

                if (Value == null)
                {
                    return false;
                }

                return Convert.ToBoolean(Convert.ToInt16(Value));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static double DBNullToDouble(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0.0;
            }

            if (Value == null)
            {
                return 0.0;
            }

            if (IsNumeric(Value))
            {
                return Convert.ToDouble(Value);
            }

            return 0.0;
        }

        public static decimal DBNullToDecimal(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return 0m;
            }

            if (Value == null)
            {
                return 0m;
            }

            if (IsNumeric(Value))
            {
                return Convert.ToDecimal(Value);
            }

            return 0m;
        }

        public static DateTime StringToDate(string Value)
        {
            if (IsDateTime(Value))
            {
                return Convert.ToDateTime(Value);
            }

            return Convert.ToDateTime("2000-01-01");
        }

        public static string NothingToString(object Value)
        {
            if (Value == null)
            {
                return string.Empty;
            }

            return Convert.ToString(Value);
        }

        public static object NothingToDBNULL(object Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                {
                    return DBNull.Value;
                }
            }
            catch (Exception)
            {
            }

            if (Value == null)
            {
                return DBNull.Value;
            }

            return Value;
        }

        public static object EmptyToNothing(object Value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                return null;
            }

            return Value;
        }

        public static object NothingEmptyToDBNULL(object Value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Value)))
            {
                return DBNull.Value;
            }

            return Value;
        }

        public static object DateNothingToDBNULL(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return DBNull.Value;
            }

            if (Value == null)
            {
                return DBNull.Value;
            }

            if (Convert.ToDateTime(Value) == DateTime.MinValue)
            {
                return DBNull.Value;
            }

            return Value;
        }

        public static bool IsNumeric(object Value)
        {
            try
            {
                double result;
                return double.TryParse(Value?.ToString(), out result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDateTime(object Value)
        {
            return Value is DateTime;
        }

        public static bool IsValidDateTime(DateTime? fecha)
        {
            DateTime value = new DateTime(1901, 1, 1);
            if (!fecha.HasValue)
            {
                return false;
            }

            if (fecha == default(DateTime))
            {
                return false;
            }

            if (fecha < value)
            {
                return false;
            }

            return true;
        }

        public static string ExceptionToString(Exception Value)
        {
            try
            {
                string text = Value.Message;
                if (Value.InnerException != null)
                {
                    text = text + "-" + ExceptionToString(Value.InnerException);
                }

                return JOMAUtilities.QuitarSaltosLinea(text);
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        public static string LongToStringZeroOnLeft(long Value, short Limit)
        {
            return StringToStringZeroOnLeft(Value.ToString(), Limit);
        }

        public static string StringToStringZeroOnLeft(string Value, short Limit)
        {
            string text = Value.ToString().PadLeft(Limit, Convert.ToChar("0"));
            if (text.Length > Limit)
            {
                text = text.Substring(text.Length - Limit, Limit);
            }

            return text;
        }

        public static string SerializeXml(object obj, ref string mensaje, bool RemoveDeclarations = false)
        {
            try
            {
                using Utf8StringWriter utf8StringWriter = new Utf8StringWriter();
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.OmitXmlDeclaration = true;
                using XmlWriter xmlWriter = XmlWriter.Create(utf8StringWriter, xmlWriterSettings);
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                if (!RemoveDeclarations)
                {
                    xmlSerializer.Serialize(xmlWriter, obj);
                }
                else
                {
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add(string.Empty, obj.GetType().GetCustomAttribute<XmlRootAttribute>().Namespace);
                    xmlSerializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
                }

                return utf8StringWriter.ToString();
            }
            catch (Exception value)
            {
                mensaje = ExceptionToString(value);
                return null;
            }
        }

        public static T DeserializeXmlObject<T>(string str, ref string mensaje)
        {
            try
            {
                using StringReader textReader = new StringReader(str);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
            catch (Exception value)
            {
                mensaje = ExceptionToString(value);
                return default(T);
            }
        }

        public static T DeserializeXmlObject<T>(Stream stm, ref string mensaje)
        {
            try
            {
                using StreamReader textReader = new StreamReader(stm);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
            catch (Exception value)
            {
                mensaje = ExceptionToString(value);
                return default(T);
            }
        }

        public static string SerializeJson(object obj, ref string mensaje)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception value)
            {
                mensaje = ExceptionToString(value);
                return null;
            }
        }

        public static T? DeserializeJsonObject<T>(string str, ref string mensaje)
        {
            try
            {
                // Suprimir la advertencia usando el operador '!'
                return JsonConvert.DeserializeObject<T>(str)!;
            }
            catch (Exception ex)
            {
                mensaje = ExceptionToString(ex);
                return default;
            }
        }

        public static string StringToBase64(string Cadena)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(Cadena));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
            

        public static string EnumtoString(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
                {
                    return descriptionAttribute.Description;
                }
            }

            return value.ToString();
        }

        public static Exception StringToException(string excepcion)
        {
            if (string.IsNullOrEmpty(excepcion))
            {
                throw new Exception("Excepción vacía");
            }

            if (excepcion.Contains("ERUSER:"))
            {
                excepcion = excepcion.Replace("ERUSER:", null);
                return new JOMAUException(excepcion);
            }

            excepcion = excepcion.Replace("EREDOC:", null);
            return new Exception(excepcion);
        }
    }
}
