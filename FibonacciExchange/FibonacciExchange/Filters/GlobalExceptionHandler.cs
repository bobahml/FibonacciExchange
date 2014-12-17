using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace FibonacciExchangeApi.Filters
{
    /// <summary>
    ///  Для IExceptionLogger - System.Web
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ResponseMessageResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Fatal Error"),
                ReasonPhrase = "Fatal Error"
            });
        }
    }
}