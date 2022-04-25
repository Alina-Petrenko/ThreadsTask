using System;
using System.Threading;

namespace ManualResetEventTask
{
    /// <summary>
    /// Represents working with 'ManualResetEvent'
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// ManualResetEvent object with false value
        /// </summary>
        private static ManualResetEvent manualResetEvent = new(false);

        #endregion

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            for (var i = 1; i < 5; i++)
            {
                var myThread = new Thread(ThreadInfoOutput);
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
            Thread.Sleep(1000);
            manualResetEvent.Set();
            Thread.Sleep(1000);

            for (var i = 5; i < 10; i++)
            {
                var myThread = new Thread(ThreadInfoOutput);
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
            Thread.Sleep(1000);
            manualResetEvent.Reset();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Outputs info about Thread
        /// </summary>
        public static void ThreadInfoOutput()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} starts and calls WaitOne()");
            manualResetEvent.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} ends");
        }

        #endregion
    }
}
