using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.CROSSCUTTING.SERVICE.Extensions;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.CROSSCUTTING.SERVICE.CrossCuttingServices
{
    public class CacheCrossCuttingService : ICacheCrossCuttingService
    {
        protected readonly ILogCrossCuttingService logService;
        protected readonly IMemoryCache memoryCache;
        protected readonly IDistributedCache distributedCache;
        protected readonly string MSG_TIPOCACHENOIMPLEMENTADO = "No se encontro definición para este tipo de Cache";
        public CacheCrossCuttingService(ILogCrossCuttingService logService, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.logService = logService;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        public async Task<bool> AddObjectAsync<T>(string key, T obj, double? duration, TipoCache tipoCache)
        {
            try
            {
                return await AddObjectAsync((TipoCache)tipoCache, key, obj, duration);
            }
            catch (Exception ex)
            {
                logService.AddLog(this.GetCaller(), ex, Contants.CrossCuttingLogLevel.Error);
                if (tipoCache != TipoCache.Memory)
                    return await AddObjectAsync(key, obj, duration, TipoCache.Memory);
            }
            return false;
        }

        public async Task<bool> AddObjectAsync<T>(string key, T obj, DateTime? fechaExpira, TipoCache tipoCache)
        {
            try
            {
                return await AddObjectAsync((TipoCache)tipoCache, key, obj, fechaExpira);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await AddObjectAsync(key, obj, fechaExpira, TipoCache.Memory);
            }
            return false;
        }

        public async Task<bool> AddValueAsync(string key, object obj, double? duration, TipoCache tipoCache)
        {
            try
            {
                return await AddValueAsync((TipoCache)tipoCache, key, obj, duration);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await AddValueAsync(key, obj, duration, TipoCache.Memory);
            }
            return false;
        }

        public async Task<bool> AddValueAsync(string key, object obj, DateTime? fechaExpira, TipoCache tipoCache)
        {
            try
            {
                return await AddValueAsync((TipoCache)tipoCache, key, obj, fechaExpira);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await AddValueAsync(key, obj, fechaExpira, TipoCache.Memory);
            }
            return false;
        }

        public async Task<T> GetObjectAsync<T>(string key, TipoCache tipoCache)
        {
            try
            {
                return await GetObjectAsync<T>((TipoCache)tipoCache, key);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await GetObjectAsync<T>(key, TipoCache.Memory);
            }
            return default;
        }

        public async Task<T> GetValueAsync<T>(string key, TipoCache tipoCache)
        {
            try
            {
                return await GetValueAsync<T>((TipoCache)tipoCache, key);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await GetValueAsync<T>(key, TipoCache.Memory);
            }
            return default;
        }

        public async Task<bool> RemoveAsync(string key, TipoCache tipoCache)
        {
            try
            {
                return await RemoveAsync((TipoCache)tipoCache, key);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await RemoveAsync(key, TipoCache.Memory);
            }
            return false;
        }

        public async Task<bool> RemoveAsync(string[] keys, TipoCache tipoCache)
        {
            try
            {
                var result = false;
                foreach (string key in keys)
                {
                    result = await RemoveAsync(key, tipoCache);
                }
                return result;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                if (tipoCache != TipoCache.Memory)
                    return await RemoveAsync(keys, TipoCache.Memory);
            }
            return false;
        }

        #region METODOS DE UTILERIA

        public async Task<bool> AddObjectAsync<T>(TipoCache tipoCache, string key, T obj, double? duration)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    switch (tipoCache)
                    {
                        case TipoCache.Memory:
                            {
                                bool num = memoryCache.AddObject(key, obj, duration, ref mensaje);
                                if (!num)
                                {
                                    throw new Exception(mensaje);
                                }

                                return num;
                            }
                        case TipoCache.Distributed:
                            return await distributedCache.AddObjectAsync(key, obj, duration);
                        default:
                            throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public async Task<bool> AddObjectAsync<T>(TipoCache tipoCache, string key, T obj, DateTime? expiration)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    switch (tipoCache)
                    {
                        case TipoCache.Memory:
                            {
                                bool num = memoryCache.AddObject(key, obj, expiration, ref mensaje);
                                if (!num)
                                {
                                    throw new Exception(mensaje);
                                }

                                return num;
                            }
                        case TipoCache.Distributed:
                            return await distributedCache.AddObjectAsync(key, obj, expiration);
                        default:
                            throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public async Task<bool> AddValueAsync(TipoCache tipoCache, string key, object obj, double? duration)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    switch (tipoCache)
                    {
                        case TipoCache.Memory:
                            {
                                bool num = memoryCache.AddValue(key, obj, duration, ref mensaje);
                                if (!num)
                                {
                                    throw new Exception(mensaje);
                                }

                                return num;
                            }
                        case TipoCache.Distributed:
                            return await distributedCache.AddValueAsync(key, obj, duration);
                        default:
                            throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public async Task<bool> AddValueAsync(TipoCache tipoCache, string key, object obj, DateTime? expiration)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    switch (tipoCache)
                    {
                        case TipoCache.Memory:
                            {
                                bool num = memoryCache.AddValue(key, obj, expiration, ref mensaje);
                                if (!num)
                                {
                                    throw new Exception(mensaje);
                                }

                                return num;
                            }
                        case TipoCache.Distributed:
                            return await distributedCache.AddValueAsync(key, obj, expiration);
                        default:
                            throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public async Task<T> GetObjectAsync<T>(TipoCache tipoCache, string key)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    return tipoCache switch
                    {
                        TipoCache.Memory => memoryCache.GetObject<T>(key, ref mensaje) ?? throw new Exception(mensaje),
                        TipoCache.Distributed => await distributedCache.GetObjectAsync<T>(key),
                        _ => throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO),
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(T);
        }

        public async Task<T> GetValueAsync<T>(TipoCache tipoCache, string key)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    return tipoCache switch
                    {
                        TipoCache.Memory => memoryCache.GetValue<T>(key, ref mensaje) ?? throw new Exception(mensaje),
                        TipoCache.Distributed => await distributedCache.GetValueAsync<T>(key),
                        _ => throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO),
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(T);
        }

        public async Task<bool> RemoveAsync(TipoCache tipoCache, string key)
        {
            try
            {
                if (CacheParameters.ENABLE)
                {
                    key = CacheParameters.PREFIJO + key;
                    string mensaje = string.Empty;
                    switch (tipoCache)
                    {
                        case TipoCache.Memory:
                            {
                                bool num = memoryCache.Remove(key, ref mensaje);
                                if (!num)
                                {
                                    throw new Exception(mensaje);
                                }

                                return num;
                            }
                        case TipoCache.Distributed:
                            return await distributedCache.GsRemoveAsync(key);
                        default:
                            throw new Exception(MSG_TIPOCACHENOIMPLEMENTADO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        void EscribirLog(Exception ex)
        {
            if (!JOMAConversions.ExceptionToString(ex).Contains("Key not found"))
            {
                logService.AddLog(this.GetCaller(), ex, Contants.CrossCuttingLogLevel.Error);
            }
        }
        #endregion
    }
}
