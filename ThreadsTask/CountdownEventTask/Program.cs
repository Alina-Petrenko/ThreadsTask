using System;
using System.Threading;

namespace CountdownEventTask
{
    /// <summary>
    /// Represents working with 'CountdownEvent'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        static void Main()
        {
            var countdownEvent = new CountdownEvent(9);
            for (var i = 1; i < 10; i++)
            {
                new Thread((value) => {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Thread {value} in");
                    countdownEvent.Signal();
                }).Start(i);
            }
            countdownEvent.Wait();
            Console.WriteLine("Done");
        }

        #endregion
    }
}
