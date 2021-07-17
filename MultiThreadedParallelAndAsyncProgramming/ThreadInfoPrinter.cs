using System.Text;
using System.Threading;

namespace MultiThreadedParallelAndAsyncProgramming
{
    public class ThreadInfoPrinter
    {
        private static void printThreadInfo(Thread thread)
        {
            var random = new System.Random();
            StringBuilder sb = new StringBuilder();
            sb.Append($"\nThread name {thread.Name}");
            Thread.Sleep(random.Next(100, 500));
            sb.Append($"\nThreadId: {thread.ManagedThreadId}");
            sb.Append($"\nThread has started {thread.IsAlive}");
            sb.Append($"\nThread priority: {thread.Priority}");
            sb.Append($"\nThread state: {thread.ThreadState}");
            System.Console.WriteLine(sb.ToString());
        }
        public static void simpleThreadInfoPrinter()
        {
            System.Console.WriteLine("In simple thread printer");
            printThreadInfo(Thread.CurrentThread);
        }
        public static void parametrizedThreadInfoPrinter(object obj)
        {
            int a = (int) obj;
            System.Console.WriteLine($"In parametrized thread printer, parameters {a}");
            printThreadInfo(Thread.CurrentThread);
        }
    }
}