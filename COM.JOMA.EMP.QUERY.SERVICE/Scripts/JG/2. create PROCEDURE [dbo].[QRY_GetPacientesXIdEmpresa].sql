USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetTerapistasXRucEmpresa]    Script Date: 22/9/2024 14:58:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[QRY_GetPacientesXIdEmpresa]
	@IdEmpresa varchar(13)
AS
BEGIN
    SET NOCOUNT ON;
		
		select 
			Pa.Id, 
			pa.NombresApellidos as 'NombresApellidosPaciente', pa.FechaNacimiento, pa.Edad,
			pa.Escuela, pa.Curso, pa.CedulaNino as 'CedulaPaciente', pa.DireccionDomiciliaria,
			pa.TelefonoMadre, pa.TelefonoPadre, NombreMadre, pa.NombrePadre,
			pa.RepresentanteLegal, pa.EdadRepresentante, pa.CedulaRepresentante, Pa.Estado
		from  [JM].[Paciente] as Pa
		inner join [JM].[Empresa] as Em on Pa.idEmpresa = Em.Id
		where IdEmpresa= @IdEmpresa and Em.Estado = 1  and Pa.Estado = 1
END;