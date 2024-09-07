using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_JOMA_APPLICACION.Areas.Administracion.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class SucursalesController : BaseController
    {
        protected ISucursalAppServices sucursalAppServices;
        protected IConsultasAppServices consultasAppServices;

        public SucursalesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IConsultasAppServices consultasAppServices, ISucursalAppServices sucursalAppServices) : base(logService, globalDictionary)
        {
            this.consultasAppServices= consultasAppServices;
            this.sucursalAppServices = sucursalAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetSucursales(string search)
        {

            try
            {
                string? draw = Request.Form.ContainsKey("draw") ? Request.Form["draw"][0] : null;
                short startRec = Request.Form.ContainsKey("start") ? Convert.ToInt16(Request.Form["start"][0]) : (short)0;
                short pagTam = Request.Form.ContainsKey("length") ? Convert.ToInt16(Request.Form["length"][0]) : (short)0;
                var pagIdx = (startRec / (pagTam == 0 ? 1 : pagTam));
                var loginDto = GetUsuarioSesion();
                var LstSucursal = await consultasAppServices.GetSucursalesPorId(loginDto.IdCompania);
                return this.CrearRespuestaExitosa(string.Empty, new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = LstSucursal.FirstOrDefault()?.Maxrowcount ?? 0,
                    recordsFiltered = LstSucursal.FirstOrDefault()?.Maxrowcount ?? 0,
                    data = LstSucursal
                });



            }
            catch (JOMAException ex)
            {
                return this.CrearRespuestaError(ex.Message, JOMAStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return this.CrearRespuestaError(ex.Message.ToString(), JOMAStatusCode.InternalServerError, ex.Message);
            }
            finally
            {
                logService.GuardarLogs();
            }
        }
    }
}
