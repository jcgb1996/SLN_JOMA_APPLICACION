USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[INS_Terapistas]    Script Date: 24/9/2024 9:41:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[INS_Pacientes]
	@NombresApellidos      VARCHAR(255),
    @FechaNacimiento       DATE,
    @Edad                  INT,
    @DireccionDomiciliaria VARCHAR(255),
    @Escuela               VARCHAR(255),
    @Curso                 VARCHAR(100),
    @CedulaPaciente        VARCHAR(10),
    @TelefonoMadre         BIGINT,
    @TelefonoPadre         BIGINT,
    @NombreMadre           VARCHAR(100),
    @NombrePadre           VARCHAR(100),
    @RepresentanteLegal    VARCHAR(100),
    @EdadRepresentante     INT,
    @CedulaRepresentante   VARCHAR(10),
    @IdEmpresa             BIGINT,
    @UsuarioCreacion       VARCHAR(100),
    @CorreoNotificacion    VARCHAR(100),
    @IdPaciente            BIGINT OUTPUT  -- Parámetro de salida para el ID generado
AS
BEGIN
    INSERT INTO [JM].[Paciente]
        ([NombresApellidos]
        ,[FechaNacimiento]
        ,[Edad]
        ,[DireccionDomiciliaria]
        ,[Escuela]
        ,[Curso]
        ,[CedulaNino]
        ,[TelefonoMadre]
        ,[TelefonoPadre]
        ,[NombreMadre]
        ,[NombrePadre]
        ,[RepresentanteLegal]
        ,[EdadRepresentante]
        ,[CedulaRepresentante]
        ,[IdEmpresa]
        ,[UsuarioCreacion]
        ,[FechaCreacion]
        ,[Estado]
        ,[CorreoNotificacion])
    VALUES
        (@NombresApellidos
        ,@FechaNacimiento
        ,@Edad
        ,@DireccionDomiciliaria
        ,@Escuela
        ,@Curso
        ,@CedulaPaciente
        ,@TelefonoMadre
        ,@TelefonoPadre
        ,@NombreMadre
        ,@NombrePadre
        ,@RepresentanteLegal
        ,@EdadRepresentante
        ,@CedulaRepresentante
        ,@IdEmpresa
        ,@UsuarioCreacion
        ,GETDATE()
        ,1
        ,@CorreoNotificacion)

    -- Obtener el ID generado
    SET @IdPaciente = SCOPE_IDENTITY()
END
