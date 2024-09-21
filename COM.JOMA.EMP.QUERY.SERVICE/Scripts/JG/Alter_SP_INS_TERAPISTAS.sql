USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[INS_Terapistas]    Script Date: 21/9/2024 14:49:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[INS_Terapistas]
    @RolId INT,
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Email VARCHAR(100),
    @Contrasena VARCHAR(255),
    @UsuarioCreacion VARCHAR(50),
    @NombreUsuario VARCHAR(30),
    @idEmpresa INT,
    @Cedula VARCHAR(20),
    @Genero INT,
    @FechaNacimiento DATETIME,
    @TelefonoContacto VARCHAR(20),
    @TelefonoContactoEmergencia VARCHAR(20),
    @Direccion VARCHAR(255),
    @IdSucursal INT,
    @IdTipoTerapia INT,
    @id BIGINT OUTPUT   -- Modificación aquí para hacer @id una variable de salida
AS
	BEGIN


	    INSERT INTO [JM].[Usuario]
	        ([RolId],
	         [Nombre],
	         [Apellido],
	         [Email],
	         [Contrasena],
	         [UsuarioCreacion],
	         [FechaCreacion],
	         [Estado],
	         [NombreUsuario],
	         [idEmpresa],
	         [Cedula],
	         [Genero],
	         [FechaNacimiento],
	         [TelefonoContacto],
	         [TelefonoContactoEmergencia],
	         [Direccion],
	         [IdSucursal],
	         [IdTipoTerapia])
	    VALUES
	        (@RolId,
	         @Nombre,
	         @Apellido,
	         @Email,
	         @Contrasena,
	         @UsuarioCreacion,
	         GETDATE(),
	         1,
	         @NombreUsuario,
	         @idEmpresa,
	         @Cedula,
	         @Genero,
	         @FechaNacimiento,
	         @TelefonoContacto,
	         @TelefonoContactoEmergencia,
	         @Direccion,
	         @IdSucursal,
	         @IdTipoTerapia);

		set @id = SCOPE_IDENTITY();
	END;
