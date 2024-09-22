USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[UDP_Terapistas]    Script Date: 21/9/2024 17:28:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UDP_Terapistas]
	@Id bigint,
    @RolId INT,
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Email VARCHAR(100),
    @UsuarioModificacion VARCHAR(50),
    @Genero INT,
    @FechaNacimiento DATETIME,
    @TelefonoContacto VARCHAR(20),
    @TelefonoContactoEmergencia VARCHAR(20),
    @Direccion VARCHAR(255),
    @IdSucursal INT,
    @IdTipoTerapia INT, 
	@Estado bit
AS
	BEGIN
			    UPDATE [JM].[Usuario]
				   SET 
					   [RolId] = @RolId
				      ,[Nombre] = @Nombre
				      ,[Apellido] = @Apellido
				      ,[Email] = @Email
				      ,[UsuarioModificacion] = @UsuarioModificacion
				      ,[FechaModificacion] = getdate()
				      ,[Genero] = @Genero
				      ,[FechaNacimiento] = @FechaNacimiento
				      ,[TelefonoContacto] = @TelefonoContacto
				      ,[TelefonoContactoEmergencia] = @TelefonoContactoEmergencia
				      ,[Direccion] = @Direccion
				      ,[IdSucursal] = @IdSucursal
				      ,[IdTipoTerapia] = @IdTipoTerapia,
					  Estado = @Estado
				 WHERE 
					Id = @Id;
	END;
