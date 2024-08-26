using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class BaseAppServices
    {
        protected ILogCrossCuttingService logService; 
        protected GlobalDictionaryDto globalDictionary;

        public BaseAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary)
        {
            this.logService = logService;
            this.globalDictionary = globalDictionary;
        }
    }
}
