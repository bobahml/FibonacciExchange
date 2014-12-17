using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Async;
using FibonacciExchangeCommon.Calculator;
using FibonacciExchangeCommon.Model;


namespace FibonacciExchangeCommon.Concrete
{
    public class TaskManager : ITaskManager
    {
        private readonly IFiboCaculator _fiboCaculator;
        private readonly IResultSender _resultSender;
        private readonly IAsyncBackGroundWorker _asyncBackGroundWorker;
        private readonly ILog _log;
        private readonly ConcurrentDictionary<Guid, byte[]> _elements = new ConcurrentDictionary<Guid, byte[]>();

        public TaskManager(IFiboCaculator fiboCaculator, IResultSender resultSender, IAsyncBackGroundWorker asyncBackGroundWorker, ILog log)
        {
            _fiboCaculator = fiboCaculator;
            _resultSender = resultSender;
            _asyncBackGroundWorker = asyncBackGroundWorker;
            _log = log;
        }

        public void Start()
        {
            if (!_asyncBackGroundWorker.IsStarted)
                _asyncBackGroundWorker.Start();
        }


        public void Process(ProcessingElement element)
        {
            if (element == null || element.TaskId == Guid.Empty || element.Data == null)
                throw new ArgumentException("element");

            _log.WriteMessage(MessageSeverity.Info, "New request: nuber size = {0}", element.Data.Length);
            _asyncBackGroundWorker.AddItemToQueue(() => ProcessInternal(element), exception => _log.WriteMessage(MessageSeverity.Error, exception));
        }


        private void ProcessInternal(ProcessingElement element)
        {
            TryWhileNoSuccess(() =>UpdateElementValue(element), 5);
            TryWhileNoSuccess(() => _resultSender.Send(element), 5);
        }

        private byte[] UpdateElementValue(ProcessingElement element)
        {
            return element.Data = _elements.AddOrUpdate(element.TaskId,
                guid => _fiboCaculator.CalcNext(element.Data),
                (guid, curbytes) => _fiboCaculator.CalcNext(curbytes, element.Data));
        }

        private async void TryWhileNoSuccess(Action action, int maxTryCount)
        {
            for (var i = 0; i < maxTryCount; i++)
            {
                try
                {
                    action();
                    break;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    if (i < maxTryCount)
                        _log.WriteMessage(MessageSeverity.Warn, e);
                    else throw;
                }

                await Task.Delay(500).ConfigureAwait(false);   
            }
        }
    }
}
