using COM.JOMA.EMP.DOMAIN.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace COM.JOMA.EMP.DOMAIN.Constants
{
    public enum JOMAComponente
    {
        [JomaDetComponenteAttribute("CP001", "JOMAEMP_PortalWeb")]
        JomaPortalWeb
    }

    public enum JOMATipoORM : byte
    {
        EntityFramework = 1,
        Dapper = 2
    }

    public enum JOMAAmbiente : byte
    {
        [Description("pro")]
        Produccion = 1,
        [Description("qa")]
        Calidad = 2,
        [Description("dev")]
        Desarrollo = 3
    }

    /// <summary>
    /// StatusCodes.Status200OK: Usado cuando la operación se completa correctamente.En los ejemplos, esto se usa para indicar que un ítem fue obtenido, creado, actualizado o eliminado con éxito.
    ///StatusCodes.Status400BadRequest: Usado cuando la solicitud es inválida, ya sea por un ID negativo o porque los datos enviados no son válidos. Esto se maneja a través de validaciones como id <= 0 o !ModelState.IsValid.
    ///StatusCodes.Status401Unauthorized: Usado cuando el usuario intenta realizar una acción que requiere autenticación y no está autenticado, o si la autenticación es inválida o insuficiente.
    ///StatusCodes.Status403Forbidden: Usado cuando el usuario está autenticado pero no tiene los permisos necesarios para realizar la acción, como en el caso de intentar crear, actualizar o eliminar un ítem sin los roles adecuados.
    /// StatusCodes.Status404NotFound: Usado cuando el recurso solicitado no existe en la base de datos, como un ID que no corresponde a ningún ítem.
    ///StatusCodes.Status500InternalServerError: Usado para manejar cualquier error inesperado que ocurra durante la ejecución del código, como problemas de conexión a la base de datos o excepciones no manejadas.
    /// </summary>
    public enum JOMAStatusCode
    {
        Success = StatusCodes.Status200OK,
        BadRequest = StatusCodes.Status400BadRequest,
        Unauthorized = StatusCodes.Status401Unauthorized,
        Forbidden = StatusCodes.Status403Forbidden,
        NotFound = StatusCodes.Status404NotFound,
        InternalServerError = StatusCodes.Status500InternalServerError
    }

    public enum TipoCache
    {
        Memory = 1,
        Distributed
    }

    public enum JOMATipoMail
    {
        [Description("Recuperar contraseña usuario")]
        RecuperacionContrasena,
        [Description("Correo bienvenida usuario")]
        CorreoBienvenida,
        [Description("Cuando se tenga agendada una cita")]
        NotificacionCitas
    }

    public enum JOMATipoEnvioMail : byte
    {
        [Description("SMTP")]
        Smtp,
        [Description("AMAZON")]
        Amazon,
        [Description("GOOGLE")]
        Google,
        [Description("MICROSOFT")]
        Microsoft
    }

    public enum JOMATipoConsultaMail : byte
    {
        [Description("NO ENVIADOS")]
        NoEnviados = 1,
        [Description("NO CONFIRMADOS")]
        NoConfirmados = 2,
        //[Description("RESPONSABLE")]
        //Responsable = 3,
        [Description("REENVIO")]
        Reenvio = 10
    }

    public enum JOMATipoCopiaMail : byte
    {
        [Description("CON COPIA")]
        CCopia,
        [Description("CON COPIA OCULTA")]
        CCopiaOculta,
        [Description("PARA DESTINATARIO")]
        ParaDestinatario
    }


    public enum JOMAEstadoMail
    {
        [Description("Error Joma")]
        ErrorJoma = -4,
        [Description("Error Joma NA")]
        ErrorJomaNa,
        [Description("Error de conexión")]
        ErrorDeConexion,
        [Description("Correo no válido")]
        CorreoNoValido,
        [Description("No enviado")]
        NoEnviado,
        [Description("Enviado")]
        Enviado,
        [Description("Enviado parcialmente")]
        EnviadoParcialmente
    }
}
