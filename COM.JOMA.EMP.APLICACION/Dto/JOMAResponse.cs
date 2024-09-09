using COM.JOMA.EMP.DOMAIN.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto
{
    public class JOMAResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string View { get; set; }
        public JOMAStatusCode StatusCode { get; set; }
        public object? Response { get; set; }

        public JOMAResponse()
        {
            Success = DomainConstants.JOMA_SUCCESS;
            Message = DomainConstants.JOMA_MESSAGE;
            StatusCode = JOMAStatusCode.Success;
            Response = null;
            View = string.Empty;
        }
    }

    public class JOMAErrorResponse : JOMAResponse
    {
        public string? ErrorDetails { get; set; }

        public JOMAErrorResponse(string errorMessage, JOMAStatusCode statusCode = JOMAStatusCode.InternalServerError, string? errorDetails = null)
        {
            Success = false;
            Message = errorMessage;
            StatusCode = statusCode;
            ErrorDetails = errorDetails;
        }
    }
}
