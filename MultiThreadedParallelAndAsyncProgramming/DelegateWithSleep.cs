using System.Threading;

namespace MultiThreadedParallelAndAsyncProgramming
{
    // A simple delegate
    public delegate int BinaryOp(int a, int b);
    internal static class DelegateWithSleep
    {
        public static int Add (int x, int y) {
            System.Console.WriteLine($"Function Add Invoked on thread{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            return x+y;
        }

        public static int Subtratct (int x, int y)
        {
            System.Console.WriteLine($"Function Subtract Invoked on thread{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            return x-y;
        }

        public static int Multiply (int x, int y)
        {
            System.Console.WriteLine($"Function Multiply Invoked on thread{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            return x*y;
        }

        public static int Divide (int x, int y)
        {
            if (y==0)
                throw new System.DivideByZeroException();
            System.Console.WriteLine($"Function Divide Invoked on thread{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            return x/y;
        }
    }
}