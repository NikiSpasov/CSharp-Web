namespace _01.Lab_AsyncDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            //    var min = int.Parse(Console.ReadLine());
            //    var max = int.Parse(Console.ReadLine());

            //    var thread = new Thread(() =>
            //    {
            //        PrintEvenNumbers(min, max);
            //    });

            //    thread.Start();
            //    thread.Join();

            //    // Thread.Sleep(1000); current method goes to sleep!
            //    Console.WriteLine("Test");
            //}

            //private static void PrintEvenNumbers(int min, int max)
            //{
            //    for (int i = min; i <= max; i++)
            //    {
            //        if (i % 2 == 0)
            //        {
            //            Console.WriteLine(i);
            //        }
            //    }
            //}

            ///// simultaniously:

            //    var thread = new Thread(() =>
            //    {
            //        PrintNumbersSlowly();
            //    });
            //    thread.Start();

            //    while (true)
            //    {
            //        var line = Console.ReadLine();
            //        Console.WriteLine();
            //        Console.WriteLine(line);
            //        Console.WriteLine();
            //        if (line == "exit")
            //        {
            //            break;
            //        }
            //    }

            //    thread.Join();
            //}

            //private static void PrintNumbersSlowly()
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        Thread.Sleep(1000);
            //        Console.WriteLine(i);
            //    }
            //}


            ////Race condition and Lock


            //var listOfnumbers = Enumerable.Range(1, 10000).ToList();

            //for (int i = 0; i < 4; i++)
            //{
            //    var thread = new Thread(() =>
            //        {
            //            while (listOfnumbers.Count > 0)
            //            {
            //                lock (listOfnumbers) //locks the memory for this and many threads can work on it!
            //                {
            //                    if (listOfnumbers.Count == 0)
            //                    {
            //                       break; 
            //                    }
            //                }
            //                var index = listOfnumbers.Count - 1;
            //                listOfnumbers.RemoveAt(index);
            //            }
            //        }
            //    );
                
            //    thread.Start();
            //}

            /// TASKS

            int n = int.Parse(Console.ReadLine());

            PrintNumbersInRange(0, 100);
            var task = Task.Run(() =>
            {
                PrintNumbersInRange(100, 200);

            });

            Console.WriteLine("Done");
            task.Wait();

        }

        private static void PrintNumbersInRange(int num, int num2)
        {
            for (int i = num; i <= num2; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
