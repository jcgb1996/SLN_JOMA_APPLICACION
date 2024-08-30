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
using System.Text.Json;

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

        public static IActionResult CrearRespuestaExitosa(this ControllerBase controller, string message, object ObjetoRespuesta, JOMAStatusCode statusCode = JOMAStatusCode.Success)
        {
            return controller.Ok(new JOMAResponse
            {
                Message = message,
                StatusCode = statusCode,
                Response = ObjetoRespuesta
            });
        }
        public static IActionResult CrearRespuestaExitosa(this ControllerBase controller, JOMAResponse jOMAResponse)
        {
            return controller.StatusCode((int)jOMAResponse.StatusCode, jOMAResponse);
        }
        public static IActionResult CrearRespuestaExitosa(this ControllerBase controller, JOMAStatusCode statusCode = JOMAStatusCode.Success)
        {
            return controller.StatusCode((int)statusCode, new JOMAResponse());
        }


        public static IActionResult CrearRespuestaError(this ControllerBase controller, string errorMessage, JOMAStatusCode statusCode = JOMAStatusCode.InternalServerError, string? errorDetails = null)
        {
            return controller.StatusCode((int)statusCode, new JOMAErrorResponse(errorMessage, statusCode, errorDetails));
        }


        public static Task CrearRespuestaError(this HttpContext context, string errorMessage, JOMAStatusCode statusCode = JOMAStatusCode.InternalServerError, string? errorDetails = null)
        {
            var response = new JOMAErrorResponse(errorMessage, statusCode, errorDetails);

            // Configurar la respuesta HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // Serializar la respuesta a JSON
            var jsonResponse = JsonSerializer.Serialize(response);

            // Escribir la respuesta al flujo de respuesta HTTP
            return context.Response.WriteAsync(jsonResponse);
        }

    }
}
