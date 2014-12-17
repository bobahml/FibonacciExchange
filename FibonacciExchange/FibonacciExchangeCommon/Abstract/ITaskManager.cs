using FibonacciExchangeCommon.Model;

namespace FibonacciExchangeCommon.Abstract
{
  public  interface ITaskManager
  {
        void Start();
        void Process(ProcessingElement element);
    }
}
