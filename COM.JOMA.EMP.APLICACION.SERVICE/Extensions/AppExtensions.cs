using AutoMapper;
using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Dto.Response;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
