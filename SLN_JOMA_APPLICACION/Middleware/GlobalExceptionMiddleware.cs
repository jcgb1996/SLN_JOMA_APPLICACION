using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Utilities;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using SLN_COM_JOMA_APPLICACION.Extensions;
using Microsoft.AspNetCore.Mvc;
using COM.JOMA.EMP.DOMAIN.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace SLN_JOMA_APPLICACION.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var logService = context.RequestServices.GetRequiredService<ILogCrossCuttingService>();
            var globalDictionary = context.RequestServices.GetRequiredService<GlobalDictionaryDto>();
            var routeData = context.GetRouteData();
            var controllerName = routeData.Values["controller"]?.ToString();
            var actionName = routeData.Values["action"]?.ToString();


            var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"Se produjo una excepción no controlada en {controllerName}/{actionName}: {JOMAUtilities.ExceptionToString(ex)}");
            var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
            logService.GuardarLogs();

            // Crear la respuesta de error usando el método de extensión
            return context.CrearRespuestaError(Mensaje, JOMAStatusCode.InternalServerError);
        }


    }
}
