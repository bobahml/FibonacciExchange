using FibonacciExchangeCommon.Abstract;
using log4net.Config;
using StructureMap.Configuration.DSL;

namespace FibonacciExchangeCommon.Concrete
{
    public class LoggerRegistry : Registry
    {
        public LoggerRegistry()
        {
            For<ILog>().Use(context => new Log4NetImpl(context.ParentType ?? context.RootType)).AlwaysUnique();
            XmlConfigurator.Configure();
        }
    }

}
