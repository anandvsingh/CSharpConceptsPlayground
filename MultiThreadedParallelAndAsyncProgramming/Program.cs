﻿using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
namespace MultiThreadedParallelAndAsyncProgramming
{
    class Program
    {
        static bool isDone = false; //Not a good idea this is not thread safe
        static void Main(string[] args)
        {
            //SynchronousThreadTest();
            //AsynchronousDelegate();
            //AsynchronousDelegateAllProps();
        }

        private static void SynchronousThreadTest()
        {
            System.Console.WriteLine("************ Demo of Synchronous delegates with Sleep **************");
            BinaryOp b = new BinaryOp(DelegateWithSleep.Add);
            b += DelegateWithSleep.Subtratct;
            b += DelegateWithSleep.Multiply;
            b += DelegateWithSleep.Divide;
            System.Console.WriteLine($"Begining invoke on thread {Thread.CurrentThread.ManagedThreadId}");
            b.Invoke(5,2);
            System.Console.WriteLine($"Done currently on thread {Thread.CurrentThread.ManagedThreadId}");
        }
        [System.Obsolete(@"BeginInvoke and EndInvoke are not supported outside .netFramework,
                             and this code is only for learning purpose in case this code is encountered in an existing system
                             If you can, avoid this code like the plague")]
        private static void AsynchronousDelegate()
        {
            System.Console.WriteLine("*********** Demo of Asynchronous delegates with IAsyncResult *************");
            BinaryOp b = new BinaryOp(DelegateWithSleep.Add);
            //b += DelegateWithSleep.Subtratct;
            //b += DelegateWithSleep.Multiply;
            //b += DelegateWithSleep.Divide;
            System.Console.WriteLine($"Begining invoke on thread {Thread.CurrentThread.ManagedThreadId}");
            //IAsyncResult is not supported in .netCore and .net5 onwards, but it gives a runtime error and not a compile time error
            IAsyncResult ar = b.BeginInvoke(5,2, null, null);
            System.Console.WriteLine($"After invoking currently on thread {Thread.CurrentThread.ManagedThreadId}");
            int result = b.EndInvoke(ar);
            System.Console.WriteLine($"Done with all thread answer {result}, Currently on thread {Thread.CurrentThread.ManagedThreadId}");

        }
        [System.Obsolete(@"BeginInvoke and EndInvoke are not supported outside .netFramework,
                             and this code is only for learning purpose in case this code is encountered in an existing system
                             If you can, avoid this code like the plague")]
        private static void OnCompletion(IAsyncResult iar)
        {
            System.Console.WriteLine("Execution completed");
            string test = (string)iar.AsyncState;
            System.Console.WriteLine(test);
            AsyncResult ar = (AsyncResult)iar;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;
            int result = b.EndInvoke(iar);
            System.Console.WriteLine($"The result is {result}");
            isDone = true;

        }
        [System.Obsolete(@"BeginInvoke and EndInvoke are not supported outside .netFramework,
                             and this code is only for learning purpose in case this code is encountered in an existing system
                             If you can, avoid this code like the plague")]
        private static void AsynchronousDelegateAllProps()
        {
            System.Console.WriteLine("*********** Demo of Asynchronous delegates with IAsyncResult with callback and passing objects *************");
            BinaryOp b = new BinaryOp(DelegateWithSleep.Add);
            //b += DelegateWithSleep.Subtratct;
            //b += DelegateWithSleep.Multiply;
            //b += DelegateWithSleep.Divide;
            System.Console.WriteLine($"Begining invoke on thread {Thread.CurrentThread.ManagedThreadId}");
            //IAsyncResult is not supported in .netCore and .net5 onwards, but it gives a runtime error and not a compile time error
            IAsyncResult ar = b.BeginInvoke(5,2, OnCompletion, "Hello this is a string object I choose to pass");
            System.Console.WriteLine($"After invoking currently on thread {Thread.CurrentThread.ManagedThreadId}");
            while(!isDone){}
            System.Console.WriteLine($"Done with all thread, Currently on thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}