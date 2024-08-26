using AutoMapper;
using COM.JOMA.EMP.APLICACION.Dto;
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
    }
}
