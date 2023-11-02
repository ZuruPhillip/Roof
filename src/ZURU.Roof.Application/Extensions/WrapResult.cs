using System.Net;

namespace ZURU.Roof.Extensions
{
    public class WrapResult
    {
        public bool Success { get; private set; }
        public int Code { get; private set; }
        public string? Message { get; private set; }
        public object? Data { get; private set; }

        private WrapResult() { }

        public WrapResult(bool success, object? data = null, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;

            if (Success)
                Code = (int)HttpStatusCode.OK;
            else
                Code = (int)HttpStatusCode.InternalServerError;

        }

        public WrapResult SetHttpCode(HttpStatusCode code)
        {
            Code = (int)code;

            return this;
        }
    }
}
