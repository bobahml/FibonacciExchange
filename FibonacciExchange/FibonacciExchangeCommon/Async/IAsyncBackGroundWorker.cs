using System;

namespace FibonacciExchangeCommon.Async
{
    public interface IAsyncBackGroundWorker: IDisposable
    {
        void Init(int maxTaskCount);
        bool IsStarted { get; }
        void Start();
        void Stop();
        void AddItemToQueue(Action action,  Action<Exception> errorCallback = null);
    }
}