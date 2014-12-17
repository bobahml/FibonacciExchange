using System;
using System.Threading;
using FibonacciExchangeApi.DataService;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Async;
using FibonacciExchangeCommon.Bus;
using FibonacciExchangeCommon.Calculator;
using FibonacciExchangeCommon.Concrete;
using StructureMap;

namespace FibonacciExchangeApi
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            var c = new Container(x =>
            {
                x.AddRegistry<LoggerRegistry>();
                x.For<IConfigurationService>().Use<ConfigurationService>().Singleton();
                x.For<ITaskManager>().Use<TaskManager>().Singleton();
                x.For<IAsyncBackGroundWorker>().Use<AsyncBackGroundWorker>().Singleton();
                x.For<IFiboCaculator>().Use<SlowFiboCaculator>();
                x.For<IResultSender>().Use<MtBusSender>().Singleton();
            });

            var queueAddress = c.GetInstance<IConfigurationService>().GetValue("BusAddress");
            var bus = MtBusConfigurationHelper.ConfigureMassTransit(queueAddress);
            c.Inject(bus);

            return c;
        }

       
    }

    
}
