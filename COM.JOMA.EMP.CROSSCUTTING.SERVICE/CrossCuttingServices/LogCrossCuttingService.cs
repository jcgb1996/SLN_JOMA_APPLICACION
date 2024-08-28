using COM.JOMA.EMP.CROSSCUTTING.Contants;
using COM.JOMA.EMP.CROSSCUTTING.DTOs;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using System;
using System.Collections.Generic;
using System.IO;

namespace COM.JOMA.EMP.CROSSCUTTING.SERVICE.CrossCuttingServices
{
    public class LogCrossCuttingService : ILogCrossCuttingService
    {
        private readonly List<LogDiskCrossCuttingDto> _logMessages;
        private readonly string _baseLogPath = @"C:\JOMA\Logs";
        private string _nombreLog = "default";

        public LogCrossCuttingService()
        {
            _logMessages = new List<LogDiskCrossCuttingDto>();
        }

        public string? AddLog(string caller, string? nombreLog, Exception ex, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info)
        {
            try
            {
                string logId = Guid.NewGuid().ToString();
                string logMessage = $"Error en {caller} con LogId {logId} y LogName {nombreLog ?? _nombreLog}: {ex.Message}";
                _logMessages.Add(new LogDiskCrossCuttingDto
                {
                    FechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    mensaje = logMessage,
                    path = nombreLog ?? _nombreLog,
                    isSubPath = true,
                    fileName = $"{nombreLog ?? _nombreLog}.txt",
                    codigoSeguimiento = logId,
                    Level = level
                });
                return logId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string? AddLog(string caller, string? nombreLog, string mensaje, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info)
        {
            try
            {
                string logId = Guid.NewGuid().ToString();
                string logMessage = $"Mensaje en {caller} con LogId {logId} y LogName : {mensaje}";
                _logMessages.Add(new LogDiskCrossCuttingDto
                {
                    FechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    mensaje = logMessage,
                    path = nombreLog ?? _nombreLog,
                    isSubPath = true,
                    fileName = $"{nombreLog ?? _nombreLog}.txt",
                    codigoSeguimiento = logId,
                    Level = level
                });
                return logId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string? AddLog(string caller, string mensaje, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info)
        {
            return AddLog(caller, _nombreLog, mensaje, level);
        }

        public string? AddLog(string caller, Exception ex, CrossCuttingLogLevel level = CrossCuttingLogLevel.Info)
        {
            return AddLog(caller, _nombreLog, ex, level);
        }

        public void CambiarNombreLog(string nombreLog)
        {
            _nombreLog = nombreLog;
        }

        public void GuardarLogs(string? rutalog = null)
        {
            if (_logMessages.Count == 0)
                return;

            string logFilePath = rutalog ?? GetRutaLog();

            try
            {
                string? logDirectory = Path.GetDirectoryName(logFilePath);

                if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                foreach (var log in _logMessages)
                {
                    string fullPath = Path.Combine(logDirectory ?? string.Empty, log.fileName);
                    File.AppendAllText(fullPath, $"{log.FechaHora} [{log.Level}] {log.mensaje}{Environment.NewLine}");
                }

                _logMessages.Clear();
            }
            catch (Exception)
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

