﻿using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface ITerapistaAppServices
    {
        public JOMAResponse RegistrarTerapista(TerapistaReqDto pacienteReqtDto);
    }
}
