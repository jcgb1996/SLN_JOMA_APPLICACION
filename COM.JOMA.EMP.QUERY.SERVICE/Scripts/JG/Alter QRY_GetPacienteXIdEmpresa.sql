USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetPacientesXIdEmpresa]    Script Date: 22/9/2024 19:26:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[QRY_GetPacientesXIdEmpresa]
	@IdEmpresa bigint
AS
BEGIN
    SET NOCOUNT ON;
		
		select 
			Pa.Id, 
			pa.NombresApellidos as 'NombresApellidosPaciente', pa.FechaNacimiento, pa.Edad,
			pa.Escuela, pa.Curso, pa.CedulaNino as 'CedulaPaciente', pa.DireccionDomiciliaria,
			pa.TelefonoMadre, pa.TelefonoPadre, NombreMadre, pa.NombrePadre,
			pa.RepresentanteLegal, pa.EdadRepresentante, pa.CedulaRepresentante, Pa.Estado, pa.CorreoNotificacion
		from  [JM].[Paciente] as Pa
		inner join [JM].[Empresa] as Em on Pa.idEmpresa = Em.Id
		where IdEmpresa= @IdEmpresa and Em.Estado = 1  and Pa.Estado = 1
END;
