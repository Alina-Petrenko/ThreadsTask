using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace BackgroundWorkerTask
{
    /// <summary>
    /// Represents working with 'BackgroundWorker'
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// BackgroundWorker object
        /// </summary>
        private static BackgroundWorker backgroundWorker = new();

        #endregion

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        static void Main()
        {
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += Process;
            backgroundWorker.RunWorkerCompleted += CheckCompletedStatus;
            backgroundWorker.ProgressChanged += CheckProcessPercentage;
            backgroundWorker.RunWorkerAsync();

            Console.WriteLine("Enter to cancel");
            Console.ReadLine();
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs a load or cancel operation
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">DoWorkEventArgs Event</param>
        public static void Process(object sender, DoWorkEventArgs eventArgs)
        {
            var time = new Stopwatch();
            time.Start();
            for (var i = 0; i < 110; i += 10)
            {
                if (backgroundWorker.CancellationPending)
                {
                    eventArgs.Cancel = true;
                    return;
                }

                backgroundWorker.ReportProgress(i);
                Thread.Sleep(1000);
            }
            time.Stop();
            eventArgs.Result = time.ElapsedMilliseconds;
        }

        /// <summary>
        /// Writes to console result of processing
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">ProgressChangedEventArgs Event</param>
        public static void CheckProcessPercentage(object sender, ProgressChangedEventArgs eventArgs)
        {
            Console.WriteLine($"Processed ...{eventArgs.ProgressPercentage}%");
        }

        /// <summary>
        /// Checks completed status
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">RunWorkerCompletedEventArgs Event</param>
        public static void CheckCompletedStatus(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            if (eventArgs.Cancelled)
            {
                Console.WriteLine("Process was canceled");
            }
            else if (eventArgs.Error != null)
            {
                Console.WriteLine($"Exception: {eventArgs.Error}");
            }
            else
            {
                Console.WriteLine($"Done. Spent time: {eventArgs.Result}ms");
            }
        }

        #endregion
    }
}
