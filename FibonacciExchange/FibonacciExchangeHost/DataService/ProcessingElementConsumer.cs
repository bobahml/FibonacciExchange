using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Model;
using MassTransit;

namespace FibonacciExchangeHost.DataService
{
    public class ProcessingElementConsumer : Consumes<ProcessingElement>.All
    {
        private readonly ITaskManager _taskManager;

        public ProcessingElementConsumer(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public void Consume(ProcessingElement message)
        {
            _taskManager.Process(message);
        }
    }
}