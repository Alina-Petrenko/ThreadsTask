using System;
using System.Threading;

namespace AutoResetEventTask
{
    /// <summary>
    /// Represents working with 'AutoResetEvent'
    /// </summary>
    public class Program
    {
        #region Private Fields

        /// <summary>
        /// AutoResetEvent object with false value
        /// </summary>
        private static AutoResetEvent autoResetEventFalse = new(false);

        /// <summary>
        /// AutoResetEvent object with true value
        /// </summary>
        private static AutoResetEvent autoResetEventTrue = new(true);

        #endregion

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            for (var i = 1; i < 6; i++)
            {
                var myThread = new Thread(Process);
                // TODO: better to call WaitOne() here to take a look that main thread is waiting for created thread.
                // TODO: Example: http://dotnetpattern.com/threading-autoresetevent
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prints the value of a variable and the thread that modified the variable
        /// </summary>
        public static void Process()
        {
            var value = 1;
            Console.WriteLine($"{Thread.CurrentThread.Name} waits on autoResetEventTrue");
            autoResetEventTrue.WaitOne();
            for (var i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} with true: {value}");
                value++;
                Thread.Sleep(100);
            }
            autoResetEventTrue.Set();
            Thread.Sleep(3000);

            value = 1;
            Console.WriteLine($"{Thread.CurrentThread.Name} waits on autoResetEventFalse");
            autoResetEventFalse.Set();
            autoResetEventFalse.WaitOne();
            for (var i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} with false: {value}");
                value++;
                Thread.Sleep(100);
            }
            autoResetEventFalse.Set();
            Thread.Sleep(3000);
        }

        #endregion
    }
}
