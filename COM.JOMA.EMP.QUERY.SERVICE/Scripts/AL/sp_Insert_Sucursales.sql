USE [JM_COMUNICATE]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[INS_Sucursales]
			@EmpresaId int
           ,@Nombre varchar(100)
           ,@Direccion varchar(255)
           ,@Telefono varchar(20)
           ,@Email varchar(100)
           ,@UsuarioCreacion varchar(50)
           ,@Ruc varchar(13)
           ,@RepresentanteLegal varchar(200)
           ,@CedulaRepresentante varchar(10)
AS
	BEGIN
	    INSERT INTO [JM].[Sucursal]
           ([EmpresaId]
           ,[Nombre]
           ,[Direccion]
           ,[Telefono]
           ,[Email]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[Estado]
           ,[Ruc]
           ,[RepresentanteLegal]
           ,[CedulaRepresentante])
     VALUES
           (
		   @EmpresaId 
           ,@Nombre 
           ,@Direccion 
           ,@Telefono 
           ,@Email 
           ,@UsuarioCreacion 
		   , GETDATE()
		   ,1
           ,@Ruc
           ,@RepresentanteLegal
           ,@CedulaRepresentante)
	END;
