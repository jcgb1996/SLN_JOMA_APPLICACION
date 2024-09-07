using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COM.JOMA.EMP.DOMAIN.Constants;

namespace COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices
{
    public interface ICacheCrossCuttingService
    {
        Task<bool> AddValueAsync(string key, object obj, double? duration, TipoCache tipoCache = TipoCache.Memory);
        Task<bool> AddValueAsync(string key, object obj, DateTime? fechaExpira, TipoCache tipoCache =  TipoCache.Memory);
        Task<bool> AddObjectAsync<T>(string key, T obj, double? duration, TipoCache tipoCache =  TipoCache.Memory);
        Task<bool> AddObjectAsync<T>(string key, T obj, DateTime? fechaExpira, TipoCache tipoCache =  TipoCache.Memory);
        Task<T> GetValueAsync<T>(string key, TipoCache tipoCache = TipoCache.Memory);
        Task<T> GetObjectAsync<T>(string key, TipoCache tipoCache = TipoCache.Memory);
        Task<bool> RemoveAsync(string key, TipoCache tipoCache = TipoCache.Memory);
        Task<bool> RemoveAsync(string[] keys, TipoCache tipoCache = TipoCache.Memory);
    }
}
