USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_Login]    Script Date: 22/9/2024 18:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[QRY_Login]
    @Usuario VARCHAR(100),
    @Clave VARCHAR(100),
    @Cedula VARCHAR(100),
    @IpLogin VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MensajeBloqueo VARCHAR(MAX) = '¡Usuario bloqueado! Ha excedido el máximo de intentos, ingrese a la opción: ¿Olvidaste tu contraseña? Para el desbloqueo del usuario.';
    DECLARE @IdUsuario INT;
    DECLARE @ClaveActual VARCHAR(MAX);
    DECLARE @IdEmpresa INT;
    DECLARE @Bloqueado BIT;
    DECLARE @IdRol INT;
    DECLARE @NIntentoLogin INT;
    DECLARE @DIAS_CADUCA_CLAVE INT;
    DECLARE @ForzarCambioClave BIT = 0;
    DECLARE @Ruc VARCHAR(25);
    DECLARE @FechaUltimoCambio DATETIME;
    DECLARE @Error BIT = 0;
    DECLARE @MensajeError VARCHAR(MAX) = '';

    -- Obtener datos del usuario
    SELECT TOP 1
        @IdUsuario = Id,
        @ClaveActual = Contrasena,
        @Bloqueado = ISNULL(Bloqueo, 0),
        @NIntentoLogin = ISNULL(NumeroIntentos, 0),
			@IdEmpresa = IdEmpresa,
			@IdRol = RolId
    FROM [JM].[Usuario]
    WHERE NombreUsuario = @Usuario AND Cedula = @Cedula AND Estado = 1;

    IF @IdUsuario IS NULL
    BEGIN
        SET @Error = 1;
        SET @MensajeError = 'No tiene acceso para este aplicativo. Usuario o contraseña incorrecta.';
        GOTO Final;
    END

    -- Obtener RUC de la empresa
    SELECT @Ruc = Ruc
    FROM [JM].[Empresa]
    WHERE Id = @IdEmpresa AND Estado = 1;

    -- Determinar si la contraseña necesita ser cambiada
    SELECT @FechaUltimoCambio = MAX(FechaHoraCambia)
    FROM [JM].[HistoricoContrasena]
    WHERE IdUsuario = @IdUsuario;

    -- Forzar cambio de clave si no hay registros históricos
    IF @FechaUltimoCambio IS NULL
    BEGIN
        SET @ForzarCambioClave = 1;
    END
    ELSE
    BEGIN
        SELECT @DIAS_CADUCA_CLAVE = ISNULL(Valor, 0)
        FROM [JM].[ParametroEmpresa]
        WHERE ID = 'DIAS_CADUCA_CLAVE_USUARIO_INTERNO' AND IdEmpresa = @IdEmpresa;

        IF DATEDIFF(DAY, @FechaUltimoCambio, GETDATE()) >= @DIAS_CADUCA_CLAVE
        BEGIN
            SET @ForzarCambioClave = 1;
        END
    END

    -- Verificar la clave y manejar intentos
    IF @Clave != @ClaveActual OR @Bloqueado = 1
    BEGIN
        SET @Error = 1;
        SET @MensajeError = @MensajeBloqueo;
        GOTO Final;
    END

    -- Resetear intentos y actualizar estado de login
    UPDATE [JM].[Usuario]
    SET NumeroIntentos = 0, Bloqueo = 0
    WHERE Id = @IdUsuario;

Final:
    -- Devolver datos del usuario o mensaje de error
    SELECT 
        Id = @IdUsuario,
        IdCompania = @IdEmpresa,
        Ruc = @Ruc,
        Usuario = @Usuario,
        NombreRol = NULL,
        IdRol = @IdRol,
        Nombre = (SELECT Nombre FROM [JM].[Usuario] WHERE Id = @IdUsuario),
        Email = (SELECT Email FROM [JM].[Usuario] WHERE Id = @IdUsuario),
        Clave = @ClaveActual,
        ForzarCambioClave = @ForzarCambioClave,
        Error = @Error,
        Bloqueado = @Bloqueado,
        MensajeError = @MensajeError,
		Cedula = @Cedula
END
