using System;
using System.Threading.Tasks;

namespace ExceptionHandlingTask
{
    /// <summary>
    /// Represents working with 'Exception-Handling'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point to project
        /// </summary>
        private static void Main()
        {
            var rand = new Random();
            var task = Task.Factory.StartNew(() => rand.Next(1,1001) / 0);
            try
            {
                Console.WriteLine($"Result: {task.Result}");
            }
            catch (AggregateException exception)
            {
                Console.Write(exception.InnerException.Message);
            }
        }

        #endregion
    }
}
