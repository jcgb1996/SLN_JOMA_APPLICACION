USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_GetMenuPorIdUsuario]    Script Date: 22/9/2024 17:38:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[QRY_GetMenuPorIdRol]
    @IdRol bigint
AS
BEGIN
    SET NOCOUNT ON;

    WITH RecursiveMenu AS
    (
        SELECT 
            M.Id,
            M.Titulo,
            M.Icono,
            M.Accion,
            M.Controlador,
            M.Area,
            M.MenuPadreId,
            UM.IdRol,
            ROW_NUMBER() OVER (PARTITION BY M.Id ORDER BY M.Id) AS RowNum
        FROM 
            JM.Menu M
            INNER JOIN [JM].[MenuXRol]  UM ON M.Id = UM.MenuId
        WHERE 
            UM.IdRol = @IdRol
        UNION ALL
        SELECT 
            M.Id,
            M.Titulo,
            M.Icono,
            M.Accion,
            M.Controlador,
            M.Area,
            M.MenuPadreId,
            RM.IdRol,
            ROW_NUMBER() OVER (PARTITION BY M.Id ORDER BY M.Id) AS RowNum
        FROM 
            JM.Menu M
            INNER JOIN RecursiveMenu RM ON M.MenuPadreId = RM.Id
    )
    SELECT 
        DISTINCT Id,
        Titulo AS Title,
        Icono AS Icon,
        Accion AS Action,
        Controlador AS Controller,
        Area,
        MenuPadreId,
        IdRol AS IdUario
    FROM 
        RecursiveMenu
    WHERE RowNum = 1
    ORDER BY 
        MenuPadreId, Id;
END;
