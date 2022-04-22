using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            IEnumerable<int> firstList = Enumerable.Range(0, 50);
            var secondList = new List<char>();
            var thirdList = new List<int>();
            IEnumerable<int> fourthList = Enumerable.Range(0,100);
            var rand = new Random();
            var cancellationTokenSource = new CancellationTokenSource();

            for (var i = 0; i < 50; i++)
            {
                secondList.Add((char)rand.Next('a', 'z'));
                thirdList.Add(rand.Next(-25, 26));
            }

            Console.WriteLine("First list");
            OutputList(firstList);
            Console.WriteLine("\nSecond list");
            OutputList(secondList);
            Console.WriteLine("\nThird list");
            OutputList(thirdList);
            Console.WriteLine("\nFourth list");
            OutputList(fourthList);

            var newFirstList = Enumerable.Range(0,50).AsParallel().AsOrdered().Where(x => x % 2 == 0);
            var newSecondList = secondList.AsParallel().Select(i => i.ToString().ToUpper());
            var newThirdList = thirdList.AsParallel().Where(i => i < 0);
        
            Console.WriteLine("\nNew First list");
            OutputList(newFirstList);
            Console.WriteLine("\nNew Second list");
            OutputList(newSecondList);
            Console.WriteLine("\nNew Third list");
            newThirdList.ForAll(i => Console.Write($"{i} "));

            Thread.Sleep(1000);     
            new Thread(() => cancellationTokenSource.Cancel()).Start();

            try
            {
                var newFourthList = fourthList.AsParallel().WithCancellation(cancellationTokenSource.Token).Select(i => i * i);
                Console.WriteLine("\nNew Fourth list");
                OutputList(newFourthList);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation for fourth list was Canceled");
            }
        }

        /// <summary>
        /// Output <paramref name="list"/> to console
        /// </summary>
        /// <param name="list">List</param>
        private static void OutputList<T> (IEnumerable<T> list)
        {
            foreach (var i in list)
            {
                Console.Write($"{i} ");
            }
        }

        #endregion
    }
}
