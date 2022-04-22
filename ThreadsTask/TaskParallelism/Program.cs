using System;
using System.Threading.Tasks;

namespace TaskParallelism
{
    /// <summary>
    /// Represents working with 'Task'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry Point in project
        /// </summary>
        private static void Main()
        {
            var rand = new Random();
            var value = rand.Next(1, 1001);
            var firstTask = Task.Factory.StartNew(() => Console.WriteLine($"Task {Task.CurrentId} is process. Lambda-expression."));
            var secondTask = new Task<int>(() => Square(value));
            var taskAfterSecondTask = secondTask.ContinueWith(taskResult =>
            {
                Console.WriteLine($"Task {Task.CurrentId} is process. Task after {taskResult.Id} task");
                Console.WriteLine($"Result: {taskResult.Result}");
            });
            secondTask.Start();
            var thirdTask = Task.Factory.StartNew(_ => Write("ThirdTask"), "ThirdTaskAsync");
            Console.WriteLine(thirdTask.AsyncState);
            Task.WaitAll(firstTask, secondTask, thirdTask, taskAfterSecondTask);

            Task parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Task {Task.CurrentId} is process. Parent Task");

                Task.Factory.StartNew(() => Console.WriteLine($"Task {Task.CurrentId} is process. Detached Task"));
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Task {Task.CurrentId} is process. Child Task");
                }, TaskCreationOptions.AttachedToParent);
            });
        }

        /// <summary>
        /// Calculate square of <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Square of <paramref name="value"/></returns>
        private static int Square(int value)
        {
            Console.WriteLine($"Task {Task.CurrentId} is process. Method Square.");
            return value * value;
        }

        /// <summary>
        /// Prints <paramref name="message"/> to the console
        /// </summary>
        /// <param name="message">Message</param>
        private static void Write(string message)
        {
            Console.WriteLine($"Task {Task.CurrentId} is process. Method Write.");
            Console.WriteLine(message);
        }

        #endregion
    }
}
