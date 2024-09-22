USE [JM_COMUNICATE]
GO

INSERT INTO [JM].[ParametroEmpresa]
           ([Id]
           ,[Descripcion]
           ,[Valor]
           ,[idempresa]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[Estado])
     VALUES
           (
		    'ASUNTO_CORREO_BIENVENIDA'
           ,'Asunto correo de Bienvenida'
           ,'Bienvenido {NombreUsuario} '
           ,1
           ,'ADMIN'
           ,getdate()
           ,1)

		   INSERT INTO [JM].[ParametroEmpresa]
           ([Id]
           ,[Descripcion]
           ,[Valor]
           ,[idempresa]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[Estado])
     VALUES
           (
		    'URL_SITIO_JOMA'
           ,'Url para iniciar sesión en el portal de JOMA'
           ,'https://localhost:7235/Inicio/Login'
           ,1
           ,'ADMIN'
           ,getdate()
           ,1)


		   INSERT INTO [JM].[ParametroEmpresa]
           ([Id]
           ,[Descripcion]
           ,[Valor]
           ,[idempresa]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[Estado])
     VALUES
           (
		    'CUERPO_CORREO_BIENVENIDA'
           ,'Cuerpo body de correo de bienvenida'
           ,'<html lang="es"><head>  
<meta charset="UTF-8">  
<meta name="viewport" content="width=device-width, initial-scale=1.0">  
<title>Bienvenida</title>  
<style>      
body { font-family: ''Arial'', sans-serif; color: #333; background-color: #f3f3f3; padding: 20px; }      
.email-container { background-color: white; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); margin: 0 auto; width: 100%; max-width: 600px; }      
.header, .footer { background-color: #F6F8FA; padding: 12px; border-bottom: 1px solid #ECECEC; }      .content { margin-top: 20px; line-height: 1.5; font-size: 16px; }      
.button { background-color: #0a66c2; color: white; border: none; padding: 10px 20px; text-align: center; display: inline-block; border-radius: 20px; text-decoration: none; font-size: 18px; margin-top: 20px;  margin-bottom: 10px; }      img.logo { width: 50px; height: auto; } /* Ajusta el tamaño según tus necesidades */  
</style>  </head>  <body>  <div class="email-container">      <div class="header">          <img src="{ImagenBase64}" alt="Logo de la Empresa" class="logo">          <div>{NombreUsuario}</div>      </div>      <div class="content">
            <h2>¡Bienvenido a {NombreCentro}!</h2>
            <p>Estamos encantados de tenerte en nuestro equipo. Tu papel es fundamental en el apoyo al desarrollo y bienestar de los niños que atendemos.</p>
            <p>Para comenzar, aquí están tus credenciales temporales de acceso al sistema:</p>
            <p>Usuario: <strong>{Usuario}</strong></p>
            <p>Contraseña: <strong>{Contraseña}</strong></p>
            <p>Te invitamos a cambiar tu contraseña al iniciar sesión por primera vez. Si tienes alguna pregunta, no dudes en contactar al administrador.</p>
            <a href="{LinkBienvenida}" class="button">Iniciar Sesión</a>
        </div>      <div class="footer">          <p>© {Año} <a href="{LinkEmpresa}">{NombreEmpresa}</a> Todos los derechos reservados.</p>      </div>  </div>  </body></html>'
           ,1
           ,'ADMIN'
           ,getdate()
           ,1)



GO


