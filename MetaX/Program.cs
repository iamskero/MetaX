using System;

namespace MetaX
{
    class Program
    {
        /// <summary>
        /// Simple example, no DI, no logging...no nada
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
        
            Console.WriteLine("Hello World!");
        }
    }
}
