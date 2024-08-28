using COM.JOMA.EMP.CROSSCUTTING.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices
{
    public interface ILogCrossCuttingService
    {
        string? AddLog(string caller, string? nombreLog, Exception ex, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info);
        string? AddLog(string caller, string? nombreLog, string mensaje, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info);
        string? AddLog(string caller, string mensaje, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info);
        string? AddLog(string caller, Exception ex, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info);
        void CambiarNombreLog(string nombreLog);
        string? GetRutaLog();
        void GuardarLogs(string? rutalog = null);
        string GetLogs(bool enable = true);
    }
}
