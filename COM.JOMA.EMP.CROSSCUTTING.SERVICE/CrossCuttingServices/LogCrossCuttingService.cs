using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.CROSSCUTTING.SERVICE.CrossCuttingServices
{
    public class LogCrossCuttingService : ILogCrossCuttingService
    {
        private readonly List<string> _logMessages; // Almacena los mensajes de log en memoria
        private readonly string _baseLogPath = @"C:\JOMA\Logs";
        private string _nombreLog = "default";

        public LogCrossCuttingService()
        {
            _logMessages = new List<string>();
        }

        public string? AddLog(string caller, string? nombreLog, Exception ex)
        {
            try
            {
                string logId = Guid.NewGuid().ToString();
                string logMessage = $"Error en {caller} con LogId {logId} y LogName {nombreLog ?? _nombreLog}: {ex.Message}";
                _logMessages.Add(logMessage); // Agrega el mensaje de log en memoria
                return logId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string? AddLog(string caller, string? nombreLog, string mensaje)
        {
            try
            {
                string logId = Guid.NewGuid().ToString();
                string logMessage = $"Mensaje en {caller} con LogId {logId} y LogName : {mensaje}";
                _logMessages.Add(logMessage); // Agrega el mensaje de log en memoria
                return logId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string? AddLog(string caller, string mensaje)
        {
            return AddLog(caller, _nombreLog, mensaje);
        }

        public string? AddLog(string caller, Exception ex)
        {
            return AddLog(caller, _nombreLog, ex);
        }

        public void CambiarNombreLog(string nombreLog)
        {
            _nombreLog = nombreLog;
        }

        public void GuardarLogs(string? rutalog = null)
        {
            if (_logMessages.Count == 0)
            {
                // Si no hay logs acumulados, no hacer nada
                return;
            }

            string logFilePath = rutalog ?? GetRutaLog();

            try
            {
                string? logDirectory = Path.GetDirectoryName(logFilePath);

                if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                File.AppendAllLines(logFilePath, _logMessages);
                _logMessages.Clear();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string? GetRutaLog()
        {
            string rutaCompleta = Path.Combine(_baseLogPath, DateTime.Now.ToString("yyyy/MM/dd"));
            string archivoLog = $"{_nombreLog}.txt";
            return Path.Combine(rutaCompleta, archivoLog);
        }

        public string GetLogs(bool enable = true)
        {
            throw new NotImplementedException();
        }
    }
}
