using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Tutorial_ConsoleApp
{
    class Program
    {
        static async void Worker1 ()
        {
            await Task.Run(() =>
            {

                Console.WriteLine("Worker 1 starts...");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Worker 1 - Step " + i.ToString() + "...");
                }

                Console.WriteLine("Worker 1 stops.");
            });
        }
        static async void Worker2()
        {
            await Task.Run(() =>
            {

                Console.WriteLine("Worker 2 starts...");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Worker 2 - Step " + i.ToString() + "...");
                }

                Console.WriteLine("Worker 2 stops.");
            });
        }

        

        public static async Task DisplayTime()
        {
            //Task wait = Task.Delay(1000);

            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    //wait.Wait();
                    Thread.Sleep(500);
                    Console.WriteLine(DateTime.Now.Millisecond);
                }
            });
        }

        static void Main(string[] args)
        {
            DisplayTime();
            Worker1();
            Worker2();
            Console.WriteLine("Please enter something:");
            Console.WriteLine("Entered string: " + Console.ReadLine());
        }
    }
}
