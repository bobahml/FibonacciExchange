using System;

namespace FibonacciExchangeCommon.Model
{
    public class ProcessingElement
    {
        public ProcessingElement()
        {

        }

        public ProcessingElement(Guid taskId, byte[] data)
        {
            TaskId = taskId;
            Data = data;
        }

        public Guid TaskId { get; set; }

        public byte[] Data { get; set; }

    }

}
