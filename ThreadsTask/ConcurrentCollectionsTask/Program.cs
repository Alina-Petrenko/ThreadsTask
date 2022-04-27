﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentCollectionsTask
{
    /// <summary>
    /// Represents working with 'ConcurrentCollections'
    /// </summary>
    public class Program
    {
        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        private static void Main()
        {
            var dictionary = new ConcurrentDictionary<int, int>();
            var count = 10;
            var taskCount = 3;

            for (var i = 1; i < count+1; i++)
            {
                // TODO: better to use TryAdd
                // TODO: https://makolyte.com/csharp-add-or-overwrite-a-value-in-concurrentdictionary/
                dictionary[i] = i;
            }

            Console.WriteLine("Dictionary: ");
            foreach (var item in dictionary)
            {
                // TODO: try to use another methods of concurrentdictionary
                Console.Write($"{item} ");
            }
            Console.WriteLine("\nStack:");
            StartStack(count, taskCount);
            Console.WriteLine("Queue:");
            StartQueue(count, taskCount);
            Console.WriteLine("BlockingCollection & ConcurrentBag");
            StartBlockingCollection(count);

        }

        /// <summary>
        /// Starts Tasks for ConcurrentStack
        /// </summary>
        /// <param name="count">Collection Count</param>
        /// <param name="taskCount">Task count</param>
        private static void StartStack(int count, int taskCount)
        {
            var stack = new ConcurrentStack<int>();
            var tasks = new Task[taskCount];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Task {Task.CurrentId} started");
                    int item;
                    for (var i = 0; i < count; i++)
                    {
                        stack.Push(i);
                        Console.WriteLine($"Task {Task.CurrentId}, item {i} added");
                    }
                    for (var i = 0; i < count; i++)
                    {
                        var success = stack.TryPop(out item);
                        if (success)
                        {
                            // TODO: wrong
                            // Console.WriteLine($"Task {Task.CurrentId}, item {i} deleted");
                            // TODO: right
                            // TODO: check the results
                            Console.WriteLine($"Task {Task.CurrentId}, item {item} deleted");
                        }
                    }
                    Console.WriteLine($"Task {Task.CurrentId} ended");
                });
            }
            foreach (var task in tasks)
            {
                task.Wait();
            }
        }


        /// <summary>
        /// Starts Tasks for ConcurrentQueue
        /// </summary>
        /// <param name="count">Collection Count</param>
        /// <param name="taskCount">Task count</param>
        private static void StartQueue(int count, int taskCount)
        {
            var queue = new ConcurrentQueue<int>();
            var tasks = new Task[taskCount];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Task {Task.CurrentId} started");
                    int item;
                    for (var i = 0; i < count; i++)
                    {
                        queue.Enqueue(i);
                        Console.WriteLine($"Task {Task.CurrentId}, item {i} added");
                    }
                    for (var i = 0; i < count; i++)
                    {
                        var success = queue.TryDequeue(out item);
                        if (success)
                        {
                            Console.WriteLine($"Task {Task.CurrentId}, item {item} dequeued");
                        }
                    }
                    Console.WriteLine($"Task {Task.CurrentId} ended");
                });
            }
            foreach (var task in tasks)
            {
                task.Wait();
            }
            
            // TODO: Example of return
            /*
             Task 6, item 0 dequeued
             Task 6, item 1 dequeued
             Task 6, item 0 dequeued
             Task 6, item 2 dequeued
             */
            // TODO: How is it possible that we dequeued item 0 two times?
        }

        /// <summary>
        /// Starts Tasks for BlockingCollection
        /// </summary>
        /// <param name="count">Collection Count</param>
        private static void StartBlockingCollection(int count)
        {
            var bag = new ConcurrentBag<int>();
            var blockingCollection = new BlockingCollection<int>(bag);
            Task firstTask = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < count; ++i)
                {
                    blockingCollection.Add(i);
                    Console.WriteLine($"Task {Task.CurrentId}, item {i} added");
                }
                blockingCollection.CompleteAdding();
            });

            Task secondTask = Task.Factory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    // TODO: better to use TryTake
                    var item = blockingCollection.Take();
                    Console.WriteLine($"Task {Task.CurrentId}, item {item} took");
                }
            });
            Task.WaitAll(firstTask, secondTask);
        }

        #endregion
    }
}
