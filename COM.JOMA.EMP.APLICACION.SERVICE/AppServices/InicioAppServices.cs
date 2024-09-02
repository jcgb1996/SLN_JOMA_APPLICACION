﻿using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request;
using COM.JOMA.EMP.APLICACION.Dto.Response;
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

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class InicioAppServices : BaseAppServices, IInicioAppServices
    {
        protected IInicioQueryServices LoginQueryServices;

        public InicioAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IInicioQueryServices LoginQueryServices) : base(logService, globalDictionary)
        {
            this.logService = logService;
            this.LoginQueryServices = LoginQueryServices;
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
                //if (loginDto.Id > 0)
                //{
                //
                //    #region CONSULTAR VENTANAS Y GENERAR MENU
                //    seccion = "CONSULTAR VENTANAS Y GENERAR MENU";
                //    var resultVentana = await loginQueryService.ConsultarVentana(loginDto.Id, (byte)SitiosWebEDOC.Consulta);
                //    if (resultVentana == null) throw new Exception(DomainConstants.EDOC_WEBSITE_ERROR_VENTANA_ASOCIADA);
                //    loginDto.MenuPersonalizado = GenerarMenuPersonalizado(resultVentana);
                //    loginDto.VentanasActivasConcat = GenerarVentanaHabilitadasConcat(resultVentana);
                //    #endregion
                //
                //    #region CONSULTAR CREDENCIALES   
                //    seccion = "CONSULTAR CREDENCIALES";
                //    var query = await ConsultarCredencialesCompania(loginDto.IdCompania);
                //    if (query == null) throw new Exception(DomainConstants.EDOC_WEBSITE_ERROR_CREDENCIALES_VACIAS, null);
                //
                //    loginDto.EdocUsuarioInterno = query.EdocUsuarioInterno;
                //    loginDto.EdocClaveInterno = query.EdocClaveInterno;
                //
                //    loginDto.AuthUsuario = query.EdocUsuario;
                //    loginDto.AuthClave = query.EdocClaveEncriptada;
                //    #endregion
                //
                //}
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
