using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.CROSSCUTTING.SERVICE.CrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Interfaces;
using COM.JOMA.EMP.QUERY.SERVICE.QueryService;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using SLN_COM_JOMA_APPLICACION.Extensions;
using SLN_COM_JOMA_APPLICACION.Settings;
using SLN_JOMA_APPLICACION.Middleware;
using System.Globalization;



//Cambio de alan a prueba
try
{

    var builder = WebApplication.CreateBuilder(args);

    // Configuración de servicios de la aplicación
    DomainParameters.APP_COMPONENTE_JOMA = JOMAComponente.JomaPortalWeb;
    DomainParameters.APP_NOMBRE = $"{DomainParameters.APP_COMPONENTE_JOMA.GetNombre()} v{AppConstants.Version}";


    #region LOAD SETTINGS
    Settings settings = new Settings();
    LoadSettings(ref settings);
    #endregion




    builder.Host.UseSerilog();

    #region INJECT DATABASE
    builder.Services.AddDatabase(
        settings.GSEDOC_BR.DataSource,
        settings.GSEDOC_BR.InitialCatalog,
        settings.GSEDOC_BR.UserId,
        settings.GSEDOC_BR.Password,
        settings.GSEDOC_BR.Timeout,
        settings.GSEDOC_BR.TipoORM);
    #endregion

    // Configuración de autenticación y autorización

    // Configuración de la sesión
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Agregar servicios al contenedor
    builder.Services.AddRazorPages()
        .AddRazorRuntimeCompilation();

    builder.Services.AddMemoryCache();
    builder.Services.AddScoped<ILogCrossCuttingService, LogCrossCuttingService>();
    builder.Services.AddScoped<IInicioAppServices, InicioAppServices>();
    builder.Services.AddScoped<IInicioQueryServices, InicioQueryServices>();
    builder.Services.AddScoped<IAdministracionAppServices, AdministracionAppServices>();
    builder.Services.AddScoped<IAdministracionQueryService, AdministracionQueryService>();
    builder.Services.AddScoped<IPacienteAppServices, PacienteAppServices>();
    builder.Services.AddScoped<IPacienteQueryServices, PacienteQueryServices>();
    builder.Services.AddScoped<ITerapistaAppServices, TerapistaAppServices>();
    builder.Services.AddScoped<ITerapistaQueryServices, TerapistaQueryServices>();
    builder.Services.AddScoped<IConsultasAppServices, ConsultasAppServices>();
    builder.Services.AddScoped<IConsultasQueryServices, ConsultasQueryServices>();
    builder.Services.AddScoped<ICacheCrossCuttingService, CacheCrossCuttingService>();
    builder.Services.AddScoped<ISucursalAppServices, SucursalAppServices>();
    CacheParameters.PREFIJO = $"{DomainConstants.JOMA_PREFIJO_CACHE}_";
    CacheParameters.ENABLE = true; //cambiar este valor, por el valor ue se va a traer desde la configuración inicial
    DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA = 600; //cambiar este valor, por el valor ue se va a traer desde la configuración inicial (tiempo en segundos)
    DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA = true; //cambiar este valor, por el valor ue se va a traer desde la configuración inicial
    builder.Services.AddSingleton<LogCrossCuttingService>();
    builder.Services.AddScoped<GlobalDictionaryDto>();


    var app = builder.Build();

    // Configurar el pipeline de solicitudes HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        //app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    // Habilitar NoCacheMiddleware
    //app.UseMiddleware<NoCacheMiddleware>();

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        ServeUnknownFileTypes = true, // Permite servir archivos con extensiones no reconocidas
        DefaultContentType = "application/json", // Establece el tipo de contenido predeterminado para archivos desconocidos
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
        }
    });
    app.UseRouting();

    // Uso de autenticación y autorización
    //app.UseAuthentication();
    //app.UseAuthorization();

    // Uso de sesión
    app.UseSession();
    //app.UseMiddleware<SessionManagementMiddleware>();
    app.UseMiddleware<GlobalExceptionMiddleware>();

    // Configurar la localización
    var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("es-EC") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("es-EC"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

    // Mapear Razor Pages
    app.MapRazorPages();
    // Rutas tradicionales
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Inicio}/{controller=Login}/{action=Index}/{id?}");


    #region ESCRIBIR LOG INICIO
    app.Services.WriteLogInitApp();
    #endregion

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(JOMAUtilities.ExceptionToString(ex));
}


void LoadSettings(ref Settings settings)
{
    JOMAUtilities.SetCultureInfo(DomainConstants.EDOC_CULTUREINFO);
    string? mensaje = string.Empty;
    string? jsonSettings = File.ReadAllText(JOMAUtilities.GetFileNameAppSettings());
    settings = JOMAConversions.DeserializeJsonObject<Settings>(jsonSettings, ref mensaje)!;
    if (settings == null) throw new Exception(mensaje);
}



