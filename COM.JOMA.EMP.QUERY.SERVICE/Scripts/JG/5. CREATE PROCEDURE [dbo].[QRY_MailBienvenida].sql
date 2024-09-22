USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_MailRecuperarContrasena]    Script Date: 21/9/2024 19:39:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[QRY_MailBienvenida]
    @IdEmpresa bigint
AS
BEGIN
    SET NOCOUNT ON;

		DECLARE @AsuntoCorreo VARCHAR(MAX);
		DECLARE @UrlLogin VARCHAR(MAX);
		DECLARE @CuerpoCorreo VARCHAR(MAX);
		
		SELECT 
		    @AsuntoCorreo = CASE WHEN id = 'ASUNTO_CORREO_BIENVENIDA' THEN valor ELSE @AsuntoCorreo END,
		    @UrlLogin = CASE WHEN id = 'URL_SITIO_JOMA' THEN valor ELSE @UrlLogin END,			
		    @CuerpoCorreo = CASE WHEN id = 'CUERPO_CORREO_BIENVENIDA' THEN valor ELSE @CuerpoCorreo END
		FROM [JM].[ParametroEmpresa]
		WHERE id IN ('ASUNTO_CORREO_BIENVENIDA', 'URL_SITIO_JOMA', 'CUERPO_CORREO_BIENVENIDA')
		  AND idempresa = @IdEmpresa and estado = 1
		
		SELECT @IdEmpresa as IdEmpresa, @AsuntoCorreo AS Asunto,@UrlLogin as UrlInicio, @CuerpoCorreo AS Cuerpo;
END;
