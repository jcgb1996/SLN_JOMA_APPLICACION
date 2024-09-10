using COM.JOMA.EMP.APLICACION.SERVICE.Constants;

namespace SLN_COM_JOMA_APPLICACION.Middleware
{
    public class SessionManagementMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionManagementMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();

            // Si la sesión no está activa, redirigir al login
            if (context.Session.GetString("UsuarioLogin") == null)
            {
                if (!path.Contains($"/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN.ToLower()}") &&
                    !path.Contains($"/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_CERRARSESION.ToLower()}"))
                {
                    context.Session.Clear(); // Limpiar la sesión por seguridad
                    context.Response.Redirect($"{WebSiteConstans.JOMA_WEBSITE_AREA_INICIO}/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN}");
                    return;
                }
            }
            else
            {
                // Permitir que la ruta de cierre de sesión sea procesada sin redirigir al dashboard
                if (path.Contains($"/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_CERRARSESION.ToLower()}"))
                {
                    // Permitir el acceso a la acción de cerrar sesión sin redirigir
                    await _next(context);
                    return;
                }

                // Si la sesión está activa y el usuario intenta acceder al login, redirigir al Dashboard
                if (path.Contains($"/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN.ToLower()}"))
                {
                    context.Response.Redirect($"/{WebSiteConstans.JOMA_WEBSITE_AREA_INICIO}/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_DASHBOARD}");
                    return;
                }
            }

            // Continuar con el siguiente middleware
            await _next(context);
        }
    }

}
