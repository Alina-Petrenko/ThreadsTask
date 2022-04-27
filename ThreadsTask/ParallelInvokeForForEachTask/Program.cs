using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelTask
{
    /// <summary>
    /// Represents working with 'Parallel'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            var rand = new Random();
            var array = new int[50];
            Parallel.For(0, array.Length, i => array[i] = rand.Next(0, 1001));
            Parallel.ForEach(array, value => Console.Write($"{value} "));
            Parallel.Invoke(
            () => Console.WriteLine($"\nTask {Task.CurrentId} is processed."),
            // TODO: you can send just name of the method if there is no input parameters.
            Print,
            () => Find(array)
            );

        }

        /// <summary>
        /// Prints Info about current task
        /// </summary>
        private static void Print()
        {
            Console.WriteLine($"Task {Task.CurrentId} is processed.");
        }

        /// <summary>
        /// Finds the biggest value in <paramref name="array"/>
        /// </summary>
        /// <param name="array">Array</param>
        private static void Find(int[] array)
        {
            Console.WriteLine($"Task {Task.CurrentId} is processed.");
            var value = array.Max(x => x);
            Console.WriteLine($"The biggest value is {value}");
        }

        #endregion
    }
}
