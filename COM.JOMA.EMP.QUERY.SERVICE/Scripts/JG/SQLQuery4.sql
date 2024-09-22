USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetSucursalesXIdEmpresa]    Script Date: 22/9/2024 15:03:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[QRY_GetSucursalesXIdEmpresa]
	@IdEmpresa bigint
AS
BEGIN
		SET NOCOUNT ON;		
		select id, Nombre, Direccion, Telefono, Email, Estado from JM.Sucursal where EmpresaId = @IdEmpresa and Estado = 1
END;
