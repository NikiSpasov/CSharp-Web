namespace _03.SumPrimesInRange
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartUp
    {
        private static string result;


        public static void Main()
        {

            

            Console.WriteLine("Calculating...");
            Task.Run(() => CalculateSlowly());

            Console.WriteLine("Enter command:");

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "show")
                {
                    if (result == null)
                    {
                        Console.WriteLine("Still calculting... please wait!");
                    }
                    else
                    {
                        Console.WriteLine($"Result is: {result}");
                    }
                }

                if (line == "exit")
                {
                    break;
                }
            }
        }

        private static void CalculateSlowly()
        {
            Thread.Sleep(5000);
            result = "42";
        }

    }
}
