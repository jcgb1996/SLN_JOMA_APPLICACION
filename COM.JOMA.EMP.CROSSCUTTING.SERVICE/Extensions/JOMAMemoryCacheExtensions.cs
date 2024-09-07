using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.CROSSCUTTING.SERVICE.Extensions
{
    public static class JOMAMemoryCacheExtensions
    {
        public static bool AddBytes(this IMemoryCache cache, string key, byte[] obj, double? duration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(duration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static bool AddBytes(this IMemoryCache cache, string key, byte[] obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(expiration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static bool AddValue(this IMemoryCache cache, string key, object obj, double? duration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(duration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static bool AddValue(this IMemoryCache cache, string key, object obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(expiration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static bool AddObject(this IMemoryCache cache, string key, object obj, double? duration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(duration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static bool AddObject<T>(this IMemoryCache cache, string key, T obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                MemoryCacheEntryOptions memoryCacheEntryOptions = GetMemoryCacheEntryOptions(expiration);
                if (memoryCacheEntryOptions != null)
                {
                    cache.Set(key, obj, memoryCacheEntryOptions);
                }
                else
                {
                    cache.Set(key, obj);
                }

                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        public static T GetValue<T>(this IMemoryCache cache, string key, ref string mensaje)
        {
            try
            {
                return (T)Convert.ChangeType(cache.Get(key) ?? throw new Exception("Key not found"), typeof(T));
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return default(T);
            }
        }

        public static byte[] GetBytes(this IMemoryCache cache, string key, ref string mensaje)
        {
            try
            {
                return (byte[])Convert.ChangeType(cache.Get(key) ?? throw new Exception("Key not found"), typeof(byte[]));
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return null;
            }
        }

        public static T GetObject<T>(this IMemoryCache cache, string key, ref string mensaje)
        {
            try
            {
                return (T)Convert.ChangeType(cache.Get(key) ?? throw new Exception("Key not found"), typeof(T));
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return default(T);
            }
        }

        public static bool Remove(this IMemoryCache cache, string key, ref string mensaje)
        {
            try
            {
                cache.Remove(key);
                return true;
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return false;
            }
        }

        private static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(double? duration)
        {
            if (duration.HasValue && duration > 0.0)
            {
                return new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(duration.Value)
                };
            }

            return null;
        }

        private static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(DateTime? expiration)
        {
            if (expiration.HasValue && expiration > DateTime.Now)
            {
                return new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expiration
                };
            }

            return null;
        }
    }
}
