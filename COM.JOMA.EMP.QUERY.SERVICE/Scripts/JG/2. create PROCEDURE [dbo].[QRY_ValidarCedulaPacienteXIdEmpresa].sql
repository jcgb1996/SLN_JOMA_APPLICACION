USE [JM_COMUNICATE]
GO
/****** Object:  StoredProcedure [dbo].[QRY_ValidaTerapistaXCedulaXCorreo]    Script Date: 24/9/2024 8:27:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[QRY_ValidarCedulaPacienteXIdEmpresa]
    @Cedula varchar(12),
	@CorreoNotificacion varchar(100),
	@IdEmpresa bigint
AS
BEGIN
    SET NOCOUNT ON;

		declare @ExisteCedula bit
		declare @ExisteCorreo bit
		 if exists(select 1 from jm.Paciente pa where pa.CedulaNino = @Cedula  and pa.IdEmpresa = @IdEmpresa  and Estado = 1)
			set @ExisteCedula = 1

		if exists(select 1 from jm.Paciente pa where pa.IdEmpresa = @IdEmpresa and pa.CorreoNotificacion= @CorreoNotificacion  and Estado = 1)
			set @ExisteCorreo = 1

		select isnull(@ExisteCedula, 0) as 'ExisteCedula', isnull(@ExisteCorreo,0) as 'ExisteCorreo'

		
END;