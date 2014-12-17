
using FibonacciExchangeCommon.Model;

namespace FibonacciExchangeCommon.Abstract
{
    public interface IResultSender 
    {
        void Send(ProcessingElement element);
    }
}
