using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.CROSSCUTTING.SERVICE.Extensions
{
    public static class JOMADistributedCacheExtensions
    {
        public static bool AddBytes(this IDistributedCache cache, string key, byte[] obj, double? duration, ref string mensaje)
        {
            try
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.Set(key, obj, distributedCacheEntryOptions);
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

        public static async Task<bool> AddBytesAsync(this IDistributedCache cache, string key, byte[] obj, double? duration)
        {
            _ = 1;
            try
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetAsync(key, obj);
                }
                else
                {
                    await cache.SetAsync(key, obj, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddBytes(this IDistributedCache cache, string key, byte[] obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.Set(key, obj, distributedCacheEntryOptions);
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

        public static async Task<bool> AddBytesAsync(this IDistributedCache cache, string key, byte[] obj, DateTime? expiration)
        {
            _ = 1;
            try
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetAsync(key, obj);
                }
                else
                {
                    await cache.SetAsync(key, obj, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddValue(this IDistributedCache cache, string key, object obj, double? duration, ref string mensaje)
        {
            try
            {
                string value = obj.ToString();
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.SetString(key, value, distributedCacheEntryOptions);
                }
                else
                {
                    cache.SetString(key, value);
                }

                return true;
            }
            catch (Exception value2)
            {
                mensaje = JOMAConversions.ExceptionToString(value2);
                return false;
            }
        }

        public static async Task<bool> AddValueAsync(this IDistributedCache cache, string key, object obj, double? duration)
        {
            _ = 1;
            try
            {
                string value = obj.ToString();
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetStringAsync(key, value);
                }
                else
                {
                    await cache.SetStringAsync(key, value, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddValue(this IDistributedCache cache, string key, object obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                string value = obj.ToString();
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.SetString(key, value, distributedCacheEntryOptions);
                }
                else
                {
                    cache.SetString(key, value);
                }

                return true;
            }
            catch (Exception value2)
            {
                mensaje = JOMAConversions.ExceptionToString(value2);
                return false;
            }
        }

        public static async Task<bool> AddValueAsync(this IDistributedCache cache, string key, object obj, DateTime? expiration)
        {
            _ = 1;
            try
            {
                string value = obj.ToString();
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetStringAsync(key, value);
                }
                else
                {
                    await cache.SetStringAsync(key, value, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddObject<T>(this IDistributedCache cache, string key, T obj, double? duration, ref string mensaje)
        {
            try
            {
                string value = JsonSerializer.Serialize(obj);
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.SetString(key, value, distributedCacheEntryOptions);
                }
                else
                {
                    cache.SetString(key, value);
                }

                return true;
            }
            catch (Exception value2)
            {
                mensaje = JOMAConversions.ExceptionToString(value2);
                return false;
            }
        }

        public static async Task<bool> AddObjectAsync<T>(this IDistributedCache cache, string key, T obj, double? duration)
        {
            _ = 1;
            try
            {
                string value = JsonSerializer.Serialize(obj);
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(duration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetStringAsync(key, value);
                }
                else
                {
                    await cache.SetStringAsync(key, value, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddObject<T>(this IDistributedCache cache, string key, T obj, DateTime? expiration, ref string mensaje)
        {
            try
            {
                string value = JsonSerializer.Serialize(obj);
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions != null)
                {
                    cache.SetString(key, value, distributedCacheEntryOptions);
                }
                else
                {
                    cache.SetString(key, value);
                }

                return true;
            }
            catch (Exception value2)
            {
                mensaje = JOMAConversions.ExceptionToString(value2);
                return false;
            }
        }

        public static async Task<bool> AddObjectAsync<T>(this IDistributedCache cache, string key, T obj, DateTime? expiration)
        {
            _ = 1;
            try
            {
                string value = JsonSerializer.Serialize(obj);
                DistributedCacheEntryOptions distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expiration);
                if (distributedCacheEntryOptions == null)
                {
                    await cache.SetStringAsync(key, value);
                }
                else
                {
                    await cache.SetStringAsync(key, value, distributedCacheEntryOptions);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetValue<T>(this IDistributedCache cache, string key, ref string mensaje)
        {
            try
            {
                string @string = cache.GetString(key);
                if (string.IsNullOrEmpty(@string))
                {
                    throw new Exception("Key not found");
                }

                return (T)Convert.ChangeType(@string, typeof(T));
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return default(T);
            }
        }

        public static async Task<T> GetValueAsync<T>(this IDistributedCache cache, string key)
        {
            try
            {
                string value = await cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Key not found");
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] GetBytes(this IDistributedCache cache, string key, ref string mensaje)
        {
            try
            {
                return cache.Get(key) ?? throw new Exception("Key not found");
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return null;
            }
        }

        public static async Task<byte[]> GetBytesAsync(this IDistributedCache cache, string key)
        {
            try
            {
                return (await cache.GetAsync(key)) ?? throw new Exception("Key not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetObject<T>(this IDistributedCache cache, string key, ref string mensaje)
        {
            try
            {
                string @string = cache.GetString(key);
                if (string.IsNullOrEmpty(@string))
                {
                    throw new Exception("Key not found");
                }

                return JsonSerializer.Deserialize<T>(@string) ?? throw new Exception("Error Deserialize");
            }
            catch (Exception value)
            {
                mensaje = JOMAConversions.ExceptionToString(value);
                return default(T);
            }
        }

        public static async Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key)
        {
            try
            {
                string obj = await cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(obj))
                {
                    throw new Exception("Key not found");
                }

                return JsonSerializer.Deserialize<T>(obj) ?? throw new Exception("Error Deserialize");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<bool> GsRemoveAsync(this IDistributedCache cache, string key)
        {
            try
            {
                await cache.RemoveAsync(key);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool GsRemove(this IDistributedCache cache, string key, ref string mensaje)
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

        private static DistributedCacheEntryOptions GetDistributedCacheEntryOptions(double? duration)
        {
            if (duration.HasValue && duration > 0.0)
            {
                return new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(duration.Value)
                };
            }

            return null;
        }

        private static DistributedCacheEntryOptions GetDistributedCacheEntryOptions(DateTime? expiration)
        {
            if (expiration.HasValue && expiration > DateTime.Now)
            {
                return new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expiration
                };
            }

            return null;
        }
    }
}
