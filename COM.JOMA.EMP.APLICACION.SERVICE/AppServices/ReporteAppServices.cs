using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class ReporteAppServices : BaseAppServices
    {
        public ReporteAppServices(ILogCrossCuttingService? logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }
    }
}
