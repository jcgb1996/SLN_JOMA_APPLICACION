using AutoMapper;
using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using COM.JOMA.EMP.APLICACION.Dto.Response.Inicio;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;

namespace COM.JOMA.EMP.APLICACION.SERVICE.Extensions
{
    public static class AppExtensions
    {
        internal static LoginAppResultDto MapToLoginAppDto(this LoginQueryDto obj)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LoginQueryDto, LoginAppResultDto>());
            var mapper = configuration.CreateMapper();
            return mapper.Map<LoginAppResultDto>(obj);
        }

        internal static List<MenuAppDto> MapToMenuAppDto(this List<MenuQueryDto> obj)
        {
            // Configurar AutoMapper para mapear MenuQueryDto a MenuAppDto
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuQueryDto, MenuAppDto>();
            });

            var mapper = configuration.CreateMapper();

            // Mapear la lista de MenuQueryDto a una lista de MenuAppDto
            return mapper.Map<List<MenuAppDto>>(obj);
        }

        internal static Paciente MapToPacienteReqDto(this PacienteReqtDto obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PacienteReqtDto, Paciente>();
            });

            var mapper = configuration.CreateMapper();

            return mapper.Map<Paciente>(obj);
        }

        internal static Paciente MapToEditarPacienteReqDto(this EditarPacienteReqDto obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditarPacienteReqDto, Paciente>();
            });

            var mapper = configuration.CreateMapper();

            return mapper.Map<Paciente>(obj);
        }

        internal static Terapista MapToTerapistaReqDto(this TerapistaReqDto obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TerapistaReqDto, Terapista>();
            });

            var mapper = configuration.CreateMapper();

            return mapper.Map<Terapista>(obj);
        }

        internal static EnvioMailAppDto MapToEnvioMailAppDto(this MailRecuperarContrasenaQueryDto obj, JOMATipoMail TipoMail)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<MailRecuperarContrasenaQueryDto, EnvioMailAppDto>()
                .ForMember(dest => dest.Asunto, m => m.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.Cuerpo, m => m.MapFrom(src => src.Cuerpo))
                .ForMember(dest => dest.Destinatario, m => m.MapFrom(src => src.Destinatario))
                .ForMember(dest => dest.IdEmpresa, m => m.MapFrom(src => src.IdEmpresa))
                .ForMember(dest => dest.IdMail, m => m.MapFrom(src => 0))
                .ForMember(dest => dest.TipoConsultaMail, m => m.MapFrom(src => JOMATipoConsultaMail.Reenvio))
                .ForMember(dest => dest.RucCompania, m => m.MapFrom(src => src.RucCompania))
                .ForMember(dest => dest.TipoMail, m => m.MapFrom(src => (byte)TipoMail))
                .ForMember(dest => dest.TipoEnvioMail, m => m.MapFrom(src => (JOMATipoEnvioMail)src.TipoEnvioMail))
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<EnvioMailAppDto>(obj);
        }
        internal static ConfigServidorCorreoAppDto MapToConfigServidorCorreoAppDto(this ServidorCorreoQueryDto obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<ServidorCorreoQueryDto, ConfigServidorCorreoAppDto>()
                .ForMember(dest => dest.CorreoMostrar, m => m.MapFrom(src => src.CorreoMostrar))
                .ForMember(dest => dest.ServidorCorreo, m => m.MapFrom(src => (JOMATipoEnvioMail)JOMAConversions.DBNullToByte(src.TipoEnvio)))
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<ConfigServidorCorreoAppDto>(obj);
        }
        internal static ConfigServidorCorreoAppDto MapToConfigServidorCorreoAppDto(this ConfigServidorCorreoQueryDto obj)
        {
            var result = obj.ServidorCorreoEmision.MapToConfigServidorCorreoAppDto();
            result.EnvioCopiaMail = (JOMATipoCopiaMail)JOMAConversions.DBNullToByte(obj.ServidorCorreoEmision.FormaCopiaMail);
            result.IntervaloTiempoEsperaEnvioMail = DomainParameters.MAIL_INTERVALO_TIEMPOESPERAENVIOMAIL;
            return result;
        }
        internal static TrazabilidadCorreo MapToMailUpdate(this EnvioMailAppDto obj, ConfigServidorCorreoAppDto ConfigCorreo, string? mensajeerror)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<EnvioMailAppDto, TrazabilidadCorreo>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.IdMail))
                .ForMember(dest => dest.MensajeError, m => m.MapFrom(src => mensajeerror))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.EstadoEnvioMail))
                .ForMember(dest => dest.Asunto, m => m.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.Cuerpo, m => m.MapFrom(src => src.Cuerpo))
                .ForMember(dest => dest.TipoMail, m => m.MapFrom(src => src.TipoMail))
                .ForMember(dest => dest.TieneAdjunto, m => m.MapFrom(src => src.TieneAdjunto))
                .ForMember(dest => dest.Destinatario, m => m.MapFrom(src => src.Destinatario))
                .ForMember(dest => dest.NombreMostrar, m => m.MapFrom(src => ConfigCorreo.NombreMostrar))
                .ForMember(dest => dest.CorreoMostrar, m => m.MapFrom(src => ConfigCorreo.CorreoMostrar))
                .ForMember(dest => dest.RucCompania, m => m.MapFrom(src => src.RucCompania))
                .ForMember(dest => dest.IdEmpresa, m => m.MapFrom(src => src.IdEmpresa))
                .ForMember(dest => dest.EMailPara, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Para)))
                .ForMember(dest => dest.EMailCc, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CC)))
                .ForMember(dest => dest.EMailCco, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CCO)))
                .ForMember(dest => dest.EMailErroneos, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Erroneos)))
                .ForMember(dest => dest.EMailAsunto, m => m.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.FechaEnvio, m => m.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IdTipoCorreo, m => m.MapFrom(src => (int)ConfigCorreo.ServidorCorreo))
                .ForMember(dest => dest.CorreoDestinatario, m => m.MapFrom(src => src.Destinatario))
                .ForMember(dest => dest.IdConfiguracionCorreo, m => m.MapFrom(src => ConfigCorreo.Id))
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<TrazabilidadCorreo>(obj);
        }

        internal static TrazabilidadCorreo MapToMailInsert(this EnvioMailAppDto obj, ConfigServidorCorreoAppDto ConfigCorreo, string? mensajeerror)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<EnvioMailAppDto, TrazabilidadCorreo>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.IdMail))
                .ForMember(dest => dest.MensajeError, m => m.MapFrom(src => mensajeerror))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.EstadoEnvioMail))
                .ForMember(dest => dest.Asunto, m => m.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.Cuerpo, m => m.MapFrom(src => src.Cuerpo))
                .ForMember(dest => dest.TipoMail, m => m.MapFrom(src => src.TipoMail))
                .ForMember(dest => dest.TieneAdjunto, m => m.MapFrom(src => src.TieneAdjunto))
                .ForMember(dest => dest.Destinatario, m => m.MapFrom(src => src.Destinatario))
                .ForMember(dest => dest.NombreMostrar, m => m.MapFrom(src => ConfigCorreo.NombreMostrar))
                .ForMember(dest => dest.CorreoMostrar, m => m.MapFrom(src => ConfigCorreo.CorreoMostrar))
                .ForMember(dest => dest.RucCompania, m => m.MapFrom(src => src.RucCompania))
                .ForMember(dest => dest.IdEmpresa, m => m.MapFrom(src => src.IdEmpresa))
                .ForMember(dest => dest.EMailPara, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Para)))
                .ForMember(dest => dest.EMailCc, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CC)))
                .ForMember(dest => dest.EMailCco, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CCO)))
                .ForMember(dest => dest.EMailErroneos, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Erroneos)))
                .ForMember(dest => dest.EMailAsunto, m => m.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.FechaEnvio, m => m.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IdTipoCorreo, m => m.MapFrom(src => (int)ConfigCorreo.ServidorCorreo))
                .ForMember(dest => dest.CorreoDestinatario, m => m.MapFrom(src => src.Destinatario))
                .ForMember(dest => dest.IdConfiguracionCorreo, m => m.MapFrom(src => ConfigCorreo.Id))
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<TrazabilidadCorreo>(obj);
        }

        internal static TrazabilidadCorreo MapToMailUpdate(this EnvioMailAppDto obj, string? mensajeerror)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<EnvioMailAppDto, TrazabilidadCorreo>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.IdMail))
                .ForMember(dest => dest.MensajeError, m => m.MapFrom(src => mensajeerror))
                //.ForMember(dest => dest.Acuse, m => m.MapFrom(src => src.AcuseRecibido))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.EstadoEnvioMail))
                .ForMember(dest => dest.EMailPara, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Para)))
                .ForMember(dest => dest.EMailCc, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CC)))
                .ForMember(dest => dest.EMailCco, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.CCO)))
                .ForMember(dest => dest.EMailErroneos, m => m.MapFrom(src => string.Join(";", src.DestinatarioFINAL.Erroneos)))
                .ForMember(dest => dest.EMailAsunto, m => m.MapFrom(src => src.Asunto))
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<TrazabilidadCorreo>(obj);
        }

        internal static ValidarUsuarioAppDto MapToValidarUsuarioAppDto(this ValidacionUsuarioQueryDto obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<ValidacionUsuarioQueryDto, ValidarUsuarioAppDto>()
                ;
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<ValidarUsuarioAppDto>(obj);
        }
    }
}
