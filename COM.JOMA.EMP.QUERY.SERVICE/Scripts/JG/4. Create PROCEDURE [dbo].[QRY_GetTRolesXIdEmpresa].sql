USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetTipoTerapiaXIdEmpresa]    Script Date: 22/9/2024 16:18:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[QRY_GetTRolesXIdEmpresa]
	@IdEmpresa bigint
AS
BEGIN
    SET NOCOUNT ON;
		
		select id, Nombre, Descripcion from [JM].Rol where  Estado = 1
END;
