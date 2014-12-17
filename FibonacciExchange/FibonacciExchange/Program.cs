using System;
using FibonacciExchangeCommon.Abstract;
using Microsoft.Owin.Hosting;

namespace FibonacciExchangeApi
{
    class Program
    {
        static void Main()
        {
            try
            {
                var cs = SmObjectFactory.Container.GetInstance<IConfigurationService>();
                var baseAddress = cs.GetValue("BaseApiAddress");
                using (WebApp.Start<Startup>(url: baseAddress))
                {
                    Console.WriteLine("Server started at " + baseAddress);
                    Console.WriteLine("Press |Enter| or |Ctrl|+|C| to exit...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
