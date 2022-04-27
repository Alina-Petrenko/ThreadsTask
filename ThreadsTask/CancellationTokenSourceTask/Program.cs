using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenSourceTask
{
    /// <summary>
    /// TODO: change description
    /// Represents working with 'CountdownEvent'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            var task = new Task(() =>
            {
                for (var i = 1; i < 10; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation canceled");
                        return;
                    }
                    Console.WriteLine($"Value: {i}");
                    Thread.Sleep(300);
                }
            }, token);
            task.Start();
            Thread.Sleep(2000);
            cancelTokenSource.Cancel();
            Console.WriteLine($"Task Status: {task.Status}");
            Thread.Sleep(1000);
            Console.WriteLine($"Task Status: {task.Status}");
            cancelTokenSource.Dispose();
        }

        #endregion
    }
}
