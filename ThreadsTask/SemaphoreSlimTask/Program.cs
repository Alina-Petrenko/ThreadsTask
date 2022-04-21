using System;
using System.Threading;

namespace SemaphoreSlimTask
{
    /// <summary>
    /// Represents working with 'SemaphoreSlim'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        static void Main()
        {
            var semaphore = new SemaphoreSlim(2);
            for (var i = 1; i < 6; i++)
            {
                new Thread((value) =>
                {
                    Console.WriteLine($"Thread {value} wait");
                    semaphore.Wait();
                    Console.WriteLine($"Thread {value} in");
                    Thread.Sleep(2000);
                    Console.WriteLine($"Thread {value} out");
                    semaphore.Release();
                }
                    ).Start(i);
            }
        }

        #endregion
    }
}
