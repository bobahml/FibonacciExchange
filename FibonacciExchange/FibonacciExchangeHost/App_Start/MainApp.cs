using System;
using System.Numerics;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Model;
using FibonacciExchangeHost.DataService;
using MassTransit;


namespace FibonacciExchangeHost
{
    internal class MainApp : IDisposable
    {
        private readonly ITaskManager _taskManager;

        public MainApp(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public void Start(int threadCount)
        {
            if (threadCount < 1)
                throw new ArgumentOutOfRangeException("threadCount");

            _taskManager.Start();

            var startVal = BigInteger.One.ToByteArray();

            for (var i = 0; i < threadCount; i++)
            {
                _taskManager.Process(new ProcessingElement(Guid.NewGuid(), startVal));
            }
        }

        public void Dispose()
        {
            //if (_bus == null)
            //    return;

            //_bus.Dispose();
        }

        //  ~MainApp()
        //{
        //    Dispose();
        //}
    }
}
