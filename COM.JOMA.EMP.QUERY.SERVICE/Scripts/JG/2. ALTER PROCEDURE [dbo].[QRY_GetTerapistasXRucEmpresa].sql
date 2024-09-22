USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetTerapistasXRucEmpresa]    Script Date: 21/9/2024 17:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[QRY_GetTerapistasXRucEmpresa]
	@RucEmpresa varchar(13)
AS
BEGIN
    SET NOCOUNT ON;
		
		select 
			te.Id, te.Nombre, te.Apellido, te.Cedula, te.Email, ti.NombreTerapia, rol.Nombre as 'NombreRol', 
			te.TelefonoContacto, te.Estado, te.NombreUsuario, te.Contrasena, te.Genero, te.FechaNacimiento,
			te.TelefonoContacto, te.TelefonoContactoEmergencia, te.IdSucursal as IdSucursal, te.IdTipoTerapia, te.Direccion,
			te.FechaNacimiento
		from  [JM].[Usuario] as Te
		inner join [JM].[Empresa] as Em on te.idEmpresa = Em.Id
		inner join [JM].[TipoTerapia] as Ti on te.IdTipoTerapia = ti.Id and em.Id = ti.IdEmpresa
		inner join jm.Rol as Rol on te.RolId = rol.Id
		where Ruc= @RucEmpresa and Em.Estado = 1  and rol.Estado = 1
END;