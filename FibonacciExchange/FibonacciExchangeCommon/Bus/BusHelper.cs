using System;
using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration;

namespace FibonacciExchangeCommon.Bus
{
    public static class MtBusConfigurationHelper
    {
        public static IServiceBus ConfigureMassTransit(string queueAddress, Action<ServiceBusConfigurator> additionalConfiguration = null)
        {
            if (queueAddress == null)
                throw new ArgumentNullException("queueAddress");

            return ServiceBusFactory.New(sbc =>
            {
                sbc.SetPurgeOnStartup(true);
                sbc.UseRabbitMq();
                //sbc.UseLog4Net();
                sbc.ReceiveFrom(queueAddress);
                if (additionalConfiguration != null)
                    additionalConfiguration(sbc);
            });
        }
    }
}
