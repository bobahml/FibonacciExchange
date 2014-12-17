using System;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Model;
using MassTransit;

namespace FibonacciExchangeApi.DataService
{
    public class MtBusSender : IResultSender
    {
        private readonly IServiceBus _bus;

        public MtBusSender(Func<IServiceBus> busfunc)
        {
            _bus = busfunc();
        }

        public void Send(ProcessingElement message)
        {
            _bus.Publish(message);
        }

        public void Dispose()
        {
            if (_bus != null)
                _bus.Dispose();
        }


        ~MtBusSender()
        {
            Dispose();
        }

        #region private

        #endregion private
    }
}
