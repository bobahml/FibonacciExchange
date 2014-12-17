using System.Web.Http.ExceptionHandling;
using FibonacciExchangeCommon.Abstract;

namespace FibonacciExchangeApi.Filters
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var log = SmObjectFactory.Container.GetInstance<ILog>();
            log.WriteMessage(MessageSeverity.Error, context.Exception);
        }
    }
}