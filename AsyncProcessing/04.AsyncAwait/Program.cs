namespace _04.AsyncAwait
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
           
        }

        public async static void DoWork()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    SlowMethod();
                }));
            }

            await Task.WhenAll(tasks.ToArray());

            Console.WriteLine("Finished");
        }

        public async static Task SlowMethod ()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Result");
        }
    }
}
