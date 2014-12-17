using System;

namespace FibonacciExchangeCommon.Async
{
    internal class TaskDescriptor
    {
        public TaskDescriptor(Action action, Action<Exception> errorAction)
        {
            Action = action;
            ErrorAction = errorAction;
        }

        public Action Action { get; private set; }
        public Action<Exception> ErrorAction { get; private set; }
    }
}