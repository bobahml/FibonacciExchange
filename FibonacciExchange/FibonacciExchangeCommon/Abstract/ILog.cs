using System;

namespace FibonacciExchangeCommon.Abstract
{
    public interface ILog
    {
        void WriteMessage(MessageSeverity severity, string message);
        [JetBrains.Annotations.StringFormatMethod("format")]
        void WriteMessage(MessageSeverity severity, string format, params object[] args);
        void WriteMessage(MessageSeverity severity, Exception exception, string message = null);
    }

    public enum MessageSeverity
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
