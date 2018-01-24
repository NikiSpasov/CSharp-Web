namespace _02.TurnPictures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(currentDirectory + "\\Images");

            var files = directoryInfo.GetFiles();

            const string resultDir = "Result";

            if (Directory.Exists(resultDir))
            {
                Directory.Delete(resultDir, true);
            }

            Directory.CreateDirectory(resultDir);

            var tasks = new List<Task>();

            foreach (var file in files)
            {
                var task = Task.Run(() =>
                {
                    var image = Image.FromFile(file.FullName);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    image.Save($"{resultDir}\\-{file.Name}");


                    Console.WriteLine($"{file.Name} processed...");
                });

                tasks.Add(task);
            }

            //here is how to catch all exeptions:

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                foreach (var exeption in ex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            

            Console.WriteLine("Finished!");

            //returning result:

            var taskReturnInt = Task.Run(() =>
            {
                return 100;
            });

            Console.WriteLine(taskReturnInt.Result);
            Console.WriteLine(taskReturnInt.GetAwaiter().GetResult()); //use this!
        }
    }
}
