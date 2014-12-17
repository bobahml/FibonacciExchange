using System;

namespace FibonacciExchangeHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var threadCount = 1;

                int tmp;
                if (args.Length > 0 && Int32.TryParse(args[0], out tmp))
                    threadCount = tmp;

                using (var app = SmObjectFactory.Container.GetInstance<MainApp>())
                {
                    app.Start(threadCount);
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
    }


   
}
