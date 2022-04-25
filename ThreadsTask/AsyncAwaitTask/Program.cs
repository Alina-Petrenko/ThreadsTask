using System;
using System.Threading.Tasks;

namespace AsyncAwaitTask
{
    /// <summary>
    /// Represents working with 'async' and 'await'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point to project
        /// </summary>
        private static async Task Main()
        {
            await Task.Run(() => Console.WriteLine($"Task started {Task.CurrentId}"));
            var firstResult = SquareAsync(new Random().Next(0,1001));
            var secondResult = SquareAsync(new Random().Next(0, 1001));
            var thirdResult = SquareAsync(new Random().Next(0, 1001));
            var results = await Task.WhenAll(firstResult, secondResult, thirdResult);
            foreach (int result in results)
            {
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Calculates square of <paramref name="value"/>
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Returns square of value</returns>
        private static async Task<int> SquareAsync(int value)
        {
            return await Task.Run(() =>
            {
                Console.WriteLine($"Task started {Task.CurrentId}");
                return value * value;
                });
        }

        #endregion
    }
}
