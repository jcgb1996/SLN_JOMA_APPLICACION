using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.JomaExtensions
{

    public static class GSExtensions
    {
        public static string GetCaller(this object obj, [CallerMemberName] string memberName = "")
        {
            return obj.GetType().Name.Split(new char[1] { '`' })[0] + "." + memberName;
        }

        public static string GetCaller(this Type obj, [CallerMemberName] string memberName = "")
        {
            return obj.Name.Split(new char[1] { '`' })[0] + "." + memberName;
        }

        public static object GetNestedPropertyValue(this object obj, string name)
        {
            string[] array = name.Split(new char[1] { '.' });
            foreach (string name2 in array)
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(name2);
                if (property == null)
                {
                    return null;
                }

                obj = property.GetValue(obj, null);
            }

            return obj;
        }

        public static void SetNestedPropertyValue(this object obj, string name, object value)
        {
            string[] array = name.Split(new char[1] { '.' });
            foreach (string name2 in array)
            {
                if (obj == null)
                {
                    break;
                }

                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(name2);
                if (property == null)
                {
                    break;
                }

                property.SetValue(obj, value);
            }
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
