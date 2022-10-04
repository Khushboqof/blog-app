using System.Net;

namespace BlogApp.Api.Commons.Exceptions
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public StatusCodeException(HttpStatusCode httpStatus, string message) : base(message)
        {
            this.HttpStatusCode = httpStatus;
        }
    }
}
