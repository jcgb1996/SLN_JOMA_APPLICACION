using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Parameters;
using COM.JOMA.EMP.QUERY.SERVICE.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace SLN_COM_JOMA_APPLICACION.Extensions
{
    public static class WebExtensions
    {
        public static void WriteLogInitApp(this IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var logService = scope.ServiceProvider.GetService<ILogCrossCuttingService>();
                logService.AddLog($"WriteLogInitApp", $"{DomainParameters.APP_NOMBRE}", $"INICIO");
                logService.GuardarLogs();
            }
        }


        //public static IServiceCollection AddDatabaseWrite(this IServiceCollection services, string? DataSource, string? InitialCatalog, string? UserId, string? Password, long? Timeout)
        //{
        //    string mensaje = string.Empty;

        //    var BW_DataSource = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(DataSource), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        //    var BW_InitialCatalog = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(InitialCatalog), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        //    var BW_UserId = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(UserId), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        //    var BW_Timeout = Timeout == null || Timeout == 0 ? 120 : AppUtilities.DBNullToLong(Timeout);
        //    var BWCadenaConexionEDOC = JOMAUtilities.CadenaConexion(BW_DataSource,
        //                BW_InitialCatalog,
        //                BW_UserId,
        //                Password,
        //                DomainConstants.JOMA_KEYENCRIPTA,
        //                ref mensaje, BW_Timeout);
        //    if (BWCadenaConexionEDOC == null) throw new Exception("Cadena de conexión GSEDOC BW inválida");
        //    services.AddDbContext<JomaCmdContext>(options => options.UseSqlServer(BWCadenaConexionEDOC));
        //    return services;
        //}

        public static IServiceCollection AddDatabase(this IServiceCollection services, string? DataSource, string? InitialCatalog, string? UserId, string? Password, long? Timeout, byte TipoOrm)
        {
            string mensaje = string.Empty;

            var BR_DataSource = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(DataSource), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
            var BR_InitialCatalog = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(InitialCatalog), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
            var BR_UserId = JOMACrypto.DescifrarClave(JOMAConversions.DBNullToString(UserId), DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
            var BR_Timeout = Timeout == null || Timeout == 0 ? 120 : AppUtilities.DBNullToLong(Timeout);
            var BRCadenaConexionEDOC = JOMAUtilities.CadenaConexion(BR_DataSource,
                                BR_InitialCatalog,
                                BR_UserId,
                                Password,
                                DomainConstants.JOMA_KEYENCRIPTA,
                                ref mensaje, BR_Timeout);
            if (BRCadenaConexionEDOC == null) throw new Exception("Cadena de conexión GSEDOC BR inválida");
            //ENTITY FRAMEWORK
            services.AddDbContext<JomaQueryContextEF>(options => options.UseSqlServer(BRCadenaConexionEDOC));
            //DAPPER
            services.AddSingleton(s => new JomaQueryContextDP(BRCadenaConexionEDOC));
            //CENTRAL
            services.AddScoped<JomaQueryContext>();
            QueryParameters.TipoORM = (JOMATipoORM)JOMAConversions.DBNullToByte(TipoOrm);
            return services;
        }

        public static IActionResult CrearRespuestaExitosa(this ControllerBase controller, string message, JOMAStatusCode statusCode = JOMAStatusCode.Success)
        {
            return controller.Ok(new JOMAResponse
            {
                Message = message,
                StatusCode = statusCode
            });
        }

        public static IActionResult CrearRespuestaError(this ControllerBase controller, string errorMessage, JOMAStatusCode statusCode = JOMAStatusCode.InternalServerError, string? errorDetails = null)
        {
            return controller.StatusCode((int)statusCode, new JOMAErrorResponse(errorMessage, statusCode, errorDetails));
        }

        public static IActionResult CrearRespuestaExitosa(this ControllerBase controller, JOMAResponse jOMAResponse)
        {
            return controller.StatusCode((int)jOMAResponse.StatusCode, jOMAResponse);
        }


    }
}
