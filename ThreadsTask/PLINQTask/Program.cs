using System;
using System.Collections.Generic;
using System.Linq;

namespace PLINQTask
{
    /// <summary>
    /// Represents working with 'PLINQ'
    /// </summary>
    public class Program
    {

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        static void Main()
        {
            var firstList = new List<int>();
            var secondList = new List<int>();
            var thirdList = new List<int>();
            var rand = new Random();

            for (var i = 0; i < 50; i++)
            {
                firstList.Add(i);
                secondList.Add(i*2);
                thirdList.Add(rand.Next(-25, 26));
            }

            Console.WriteLine("First list");
            OutputList(firstList);
            Console.WriteLine("\nSecond list");
            OutputList(secondList);
            Console.WriteLine("\nThird list");
            OutputList(thirdList);

            var newFirstList = Enumerable.Range(0,50).AsParallel().AsOrdered().Where(x => x % 2 == 0);
            var newSecondList = secondList.AsParallel().AsOrdered().Where(i => i < 20).Select(i => i * i);
            var newThirdList = thirdList.AsParallel().Where(i => i < 0);
            Console.WriteLine("\nNew First list");
            OutputList(newFirstList);
            Console.WriteLine("\nNew Second list");
            OutputList(newSecondList);
            Console.WriteLine("\nNew Third list");
            newThirdList.ForAll(i => Console.Write($"{i} "));
        }

        /// <summary>
        /// Output <paramref name="list"/> to console
        /// </summary>
        /// <param name="list">List</param>
        private static void OutputList (IEnumerable<int> list)
        {
            foreach (var i in list)
            {
                Console.Write($"{i} ");
            }
        }

        #endregion
    }
}
