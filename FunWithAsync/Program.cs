using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithAsync
{
    class Program
    {
        //This is not in MultiThreadedParallelAndAsyncProgramming as main is also an Async here
        static async Task Main(string[] args)
        {
            Console.WriteLine("*****************Fun with Async***********");
            string message = await DoWorkAsync();
            Console.WriteLine(message);
            Console.WriteLine($"Completed");                      
        }

        private async static Task<string> DoWorkAsync()
        {
            return await Task.Run(()=> {
            Thread.Sleep(5000);
            return "Work finished";
            });
        }
    }
}
