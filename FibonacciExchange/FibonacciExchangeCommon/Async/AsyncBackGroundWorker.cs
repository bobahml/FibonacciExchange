using System;
using System.Threading;
using System.Threading.Tasks;
using HellBrick.Collections;

namespace FibonacciExchangeCommon.Async
{
    public class AsyncBackGroundWorker : IAsyncBackGroundWorker
    {
        private readonly AsyncQueue<TaskDescriptor> _asyncQueue = new AsyncQueue<TaskDescriptor>();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(Environment.ProcessorCount);
        private  CancellationTokenSource _source;
        private volatile bool _isStarted;

        public void Init(int maxTaskCount)
        {
            if (maxTaskCount<1)
                throw new ArgumentException("maxTaskCount");
            if (_isStarted)
                throw new InvalidOperationException("isStarted");

            _semaphore = new SemaphoreSlim(maxTaskCount);
        }

        public bool IsStarted { get { return _isStarted; } }
        public void Start()
        {
            StopProcessing();
            StartProcessing();
        }

        public void Stop()
        {
            StopProcessing();
        }

        public void AddItemToQueue(Action action, Action<Exception> errorCallback = null)
        {
            _asyncQueue.Add(new TaskDescriptor(action, errorCallback));
        }

        public void Dispose()
        {
            StopProcessing();
        }

        ~AsyncBackGroundWorker()
        {
            StopProcessing();
        }

        #region private
        
        private void StartProcessing()
        {
            _isStarted = true;
            _source = new CancellationTokenSource();
            Task.Factory.StartNew(() => DoProcessing(_source.Token), TaskCreationOptions.LongRunning);
        }

        private async void DoProcessing(CancellationToken token)
        {
            try
            {
                while (_isStarted)
                {
                    await _semaphore.WaitAsync(token);
                    var newTask = await _asyncQueue.TakeAsync(token).ConfigureAwait(false);
                    ProcessItem(newTask);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void StopProcessing()
        {
            _isStarted = false;
            if (_source != null)
                _source.Cancel();
            _source = null;
        }

        private async void ProcessItem(TaskDescriptor descriptor)
        {
            try
            {
                await Task.Factory.StartNew(descriptor.Action, TaskCreationOptions.LongRunning);
            }
            catch (Exception e)
            {
                if (descriptor.ErrorAction != null)
                    descriptor.ErrorAction(e);
            }
            finally
            {
                _semaphore.Release();
            }
        }
        #endregion private
    }
}
