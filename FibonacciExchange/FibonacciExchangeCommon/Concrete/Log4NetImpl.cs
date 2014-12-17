using System;
using FibonacciExchangeCommon.Abstract;
using log4net;
using log4net.Core;
using ILog = FibonacciExchangeCommon.Abstract.ILog;

namespace FibonacciExchangeCommon.Concrete
{
    public class Log4NetImpl : ILog
    {
        private readonly log4net.ILog _logger;

        public Log4NetImpl(Type typedAs)
        {
            _logger = LogManager.GetLogger(typedAs);
        }

        public void WriteMessage(MessageSeverity severity, string message)
        {
            var lvl = GetLoggingLevel(severity);

            _logger.Logger.Log(new LoggingEvent(new LoggingEventData
            {
                Level = lvl,
                TimeStamp = DateTime.Now,
                LoggerName = _logger.Logger.Name,
                Message = message
            }));
        }

        public void WriteMessage(MessageSeverity severity, string format, params object[] args)
        {
             WriteMessage(severity, string.Format(format, args));
        }

        public void WriteMessage(MessageSeverity severity, Exception exception, string message = null)
        {
            var lvl = GetLoggingLevel(severity);

            _logger.Logger.Log(new LoggingEvent(new LoggingEventData
            {
                Level = lvl,
                TimeStamp = DateTime.Now,
                LoggerName = _logger.Logger.Name,
                Message = message,
                ExceptionString = string.Format("Message: {0}{1}StackTrace: {2}", exception.Message, Environment.NewLine, exception.StackTrace)
            }));
        }

        private static Level GetLoggingLevel(MessageSeverity severity)
        {
            Level lvl;
            switch (severity)
            {
                case MessageSeverity.Debug:
                    lvl = Level.Debug;
                    break;
                case MessageSeverity.Info:
                    lvl = Level.Info;
                    break;
                case MessageSeverity.Warn:
                    lvl = Level.Warn;
                    break;
                case MessageSeverity.Error:
                    lvl = Level.Error;
                    break;
                case MessageSeverity.Fatal:
                    lvl = Level.Fatal;
                    break;
                default:
                    lvl = Level.Notice;
                    break;
            }
            return lvl;
        }
    }
}
