using System;
using System.Threading;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Async;
using FibonacciExchangeCommon.Bus;
using FibonacciExchangeCommon.Calculator;
using FibonacciExchangeCommon.Concrete;
using FibonacciExchangeHost.DataService;
using MassTransit;
using StructureMap;

namespace FibonacciExchangeHost
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder 
            = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

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
                x.For<IAsyncBackGroundWorker>().Use<AsyncBackGroundWorker>().Singleton();
                x.For<IFiboCaculator>().Use<SlowFiboCaculator>();
                x.For<ProcessingElementConsumer>().Singleton();
                x.For<ITaskManager>().Use<TaskManager>().Singleton();
                x.For<IResultSender>().Use<WebApiSender>().Singleton();
                x.For<MainApp>();
            });


            var queueAddress = c.GetInstance<IConfigurationService>().GetValue("BusAddress");
            var bus = MtBusConfigurationHelper.ConfigureMassTransit(queueAddress, sbc=> sbc.Subscribe(x => x.LoadFrom(c)));
            c.Inject(bus);

            return c;
        }
    }
}
