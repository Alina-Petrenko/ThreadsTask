using System;
using System.Threading;

namespace Locking
{
    /// <summary>
    /// Represents working with 'lock'
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// Locker
        /// </summary>
        private static readonly object _locker = new();

        #endregion

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("Using 'lock'");
            for (var i = 0; i < 5; i++)
            {
                Thread myThread = new(Increment);
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Represents increment of value
        /// </summary>
        public static void Increment()
        {
            var value = 1;
            lock (_locker)
            {
                for (var i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {value}");
                    value++;
                    // TODO: increased timeout to see that threads run in different order.
                    Thread.Sleep(500);
                }
            }
        }

        #endregion

    }
}
