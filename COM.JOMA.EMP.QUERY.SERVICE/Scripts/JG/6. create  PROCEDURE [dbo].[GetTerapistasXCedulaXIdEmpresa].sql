USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetTerapistasXIdXIdEmpresa]    Script Date: 21/9/2024 20:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[QRY_GetTerapistasXCedulaXIdEmpresa]
	@cedula varchar(10),
	@IdEmpresa bigint
AS
BEGIN
    SET NOCOUNT ON;
		
		select 
			te.Id, te.Nombre, te.Apellido, te.Cedula, te.Email, te.TelefonoContacto, te.Estado, te.NombreUsuario, te.Contrasena, 
			te.Genero, te.FechaNacimiento, te.TelefonoContacto, te.TelefonoContactoEmergencia, te.IdSucursal as IdSucursal, 
			te.IdTipoTerapia, te.Direccion		
		from  [JM].[Usuario] as Te
		inner join [JM].[Empresa] as Em on te.idEmpresa = Em.Id
		where em.Id= @IdEmpresa and te.Cedula = @cedula and Em.Estado = 1 and te.Estado = 1
END;
