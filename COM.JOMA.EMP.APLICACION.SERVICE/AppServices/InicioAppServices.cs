using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request;
using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response;
using COM.JOMA.EMP.APLICACION.Dto.Response.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response.Mail;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
using COM.JOMA.EMP.CROSSCUTTING.Contants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Interfaces;
using System;
using System.Security.Cryptography;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class InicioAppServices : BaseAppServices, IInicioAppServices
    {
        protected IInicioQueryServices LoginQueryServices;
        protected IEnvioMailEnLineaAppServices envioMailEnLineaAppServices;

        public InicioAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IInicioQueryServices LoginQueryServices, IEnvioMailEnLineaAppServices envioMailEnLineaAppServices) : base(logService, globalDictionary)
        {
            this.logService = logService;
            this.LoginQueryServices = LoginQueryServices;
            this.envioMailEnLineaAppServices = envioMailEnLineaAppServices;
        }

        public async Task<List<MenuAppDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var ListMenuQueryDtos = await LoginQueryServices.GetOpcionesMenuPorIdUsuario(IdUsuario);
                var menus = BuildMenuHierarchy(ListMenuQueryDtos);
                List<MenuAppDto> menuAppDtos = menus.MapToMenuAppDto();
                return menuAppDtos;
            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}");
                var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
                throw new Exception(Mensaje);
            }

        }

        public async Task<LoginAppResultDto> LoginCompania(LoginReqAppDto login)
        {
            LoginAppResultDto? loginAppResultDto = new();
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR LOGIN";
                var RealizoLogin = await LoginQueryServices.Login(login.Usuario, login.Clave, login.Compania);
                if (RealizoLogin is null || RealizoLogin.Count == 0) throw new JOMAException($"No se encontro datos para la compañia {login.Compania}");


                seccion = "REALIZAR MAP";
                var loginDto = RealizoLogin.First().MapToLoginAppDto();
                if (loginDto.Error) throw new JOMAException(loginDto.MensajeError);

                seccion = "CONSULTAR MENU POR ID USUARIO";
                var MenuAppDto = await GetOpcionesMenuPorIdUsuario(loginDto.Id);
                loginDto.OpcionesMenu = MenuAppDto;
                return loginDto;
            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}", CrossCuttingLogLevel.Error);
                var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
                throw new Exception(Mensaje);
            }
        }

        public async Task<EnvioMailEnLineaAppResultDto> RecuperarContrasena(RecuperacionReqAppDto recuperacionReqAppDto)
        {
           
            string seccion = string.Empty;
            try
            {
                EnvioMailEnLineaAppResultDto? MailEnviado = new();
                var ValidaUsuario = await LoginQueryServices.ValidarUsuarioRecuperacion(recuperacionReqAppDto.UsuarioRecuperacion, recuperacionReqAppDto.CedulaRecuperacion);
                if (ValidaUsuario is null) throw new JOMAException($"No existen datos para el usuario: {recuperacionReqAppDto.UsuarioRecuperacion}, con cedula {recuperacionReqAppDto.CedulaRecuperacion}");

                var ValidarAppDto = ValidaUsuario.First().MapToValidarUsuarioAppDto();

                if (ValidarAppDto.Correcto <= 0) throw new JOMAException(ValidarAppDto.Mensaje);

                MailEnviado = await envioMailEnLineaAppServices.EnviarCorreoRecuperacionContrasena(new Dto.Request.Mail.EnvioMailEnLineaRecuperacionContrasenaAppDto()
                {
                    Cedula = ValidarAppDto.CedulaUsuario,
                    Ruc = ValidarAppDto.Ruc,
                    Usuario = ValidarAppDto.NombreUsuario,
                });

                return MailEnviado;
            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region METODOS AYUDANTES
        private List<MenuQueryDto> BuildMenuHierarchy(List<MenuQueryDto> flatMenuList)
        {
            var menuHierarchy = flatMenuList
                .Where(x => x.MenuPadreId == null)
                .Select(x => new MenuQueryDto
                {
                    Id = x.Id,
                    MenuPadreId = x.MenuPadreId,
                    IdUario = x.IdUario,
                    Title = x.Title,
                    Icon = x.Icon,
                    Action = x.Action,
                    Controller = x.Controller,
                    Area = x.Area,
                    Children = GetChildren(flatMenuList, x.Id)
                })
                .ToList();

            return menuHierarchy;
        }
        private List<MenuQueryDto> GetChildren(List<MenuQueryDto> flatMenuList, int parentId)
        {
            return flatMenuList
                .Where(x => x.MenuPadreId == parentId)
                .Select(x => new MenuQueryDto
                {
                    Id = x.Id,
                    MenuPadreId = x.MenuPadreId,
                    IdUario = x.IdUario,
                    Title = x.Title,
                    Icon = x.Icon,
                    Action = x.Action,
                    Controller = x.Controller,
                    Area = x.Area,
                    Children = GetChildren(flatMenuList, x.Id)
                })
                .ToList();
        }


        #endregion
    }
}
