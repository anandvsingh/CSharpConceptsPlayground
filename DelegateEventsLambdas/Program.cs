using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace DelegateEventsLambdas
{
    
    class Program
    {
        static void Main(string[] args)
        {
            simplDelegateDriver();
            //InvokeVsDynamicInvoke(); Takes too long to run, uncomment when need to test
            delegatesAsEventEnablers();
            GenericDelegates();
            ActionAndFunc();
            DirectEvents();
            EventHandlerTUsage();
        }

        static private void simplDelegateDriver()
        {
            Console.WriteLine("\n********** Simple Delegate **************\n");
            BinaryOp b = new BinaryOp(SimpleMath.Add);
            b += SimpleMath.Multiply;
            b += SimpleMath.Subtratct;
            b += SimpleMath.Divide;
            System.Console.WriteLine(b(10,10)); //Executing all the functions in the list, the result only from the last is visible

            // To get all results as IEnumerable<T>
            // While this works avoid putting yourself in a situation where you need result from multiple Delegates as Dynamic Invoke takes a performance hit
            IEnumerable<int> results = b.GetInvocationList().Select(x => (int)x.DynamicInvoke(10, 10));
            foreach (int result in results)
            {
                System.Console.WriteLine(result);
            }
        }

        static private void InvokeVsDynamicInvoke()
        {
            System.Console.WriteLine("\n********** Invoke vs Dynamic Invoke ************\n");
            Func<int, int> twice = x => x * 2;
            const int LOOP = 5000000; // 5M
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < LOOP; i++)
            {
                twice.Invoke(3);
            }
            watch.Stop();
            Console.WriteLine("Invoke: {0}ms", watch.ElapsedMilliseconds);
            watch = Stopwatch.StartNew();
            for (int i = 0; i < LOOP; i++)
            {
                twice.DynamicInvoke(3);
            }
            watch.Stop();
            Console.WriteLine("DynamicInvoke: {0}ms", watch.ElapsedMilliseconds);
        }

        static private void delegatesAsEventEnablers()
        {
            System.Console.WriteLine("\n********* Delegates as event enablers **************\n");
            Car myCar = new Car(10, 100, "Honda");
            myCar.RegisterWithCarEngine(OnCarEngineEvent);
            for (int i = 0; i < 6; i++)
                myCar.Accelerate(20);
        }

        private static void OnCarEngineEvent(string message) => System.Console.WriteLine(message); 

        public delegate TResult genricDelegate<T, TResult> (T args); //Delegates should be declared outside a static function otherwise they end up raising a lot of issues
        static private void GenericDelegates()
        {
            System.Console.WriteLine("\n********** Generic Delegate **************\n");
            
            Func<string,string> StringDelegate =  (string value) => value + "World";
            Func<int, int> IntDelegate  = (int value) => value + 5;

            genricDelegate<string, string> strTarget = new genricDelegate<string, string> (StringDelegate);
            genricDelegate<int, int> intTarget = new genricDelegate<int, int> (IntDelegate);

            intTarget += IntDelegate2;

            System.Console.WriteLine($"{strTarget("Hello ")} {intTarget(5)}"); 
        }

        static int IntDelegate2(int x){
            return x+2;
        }
        static private void ActionAndFunc()
        {
            System.Console.WriteLine("\n********** Action and Func Delegates **************\n");
            Action<string> writeToConsole = x => System.Console.WriteLine(x);
            Func <string ,string > helloWorld = x => x + " World";
            writeToConsole(helloWorld("Hello"));
        }

        static private void DirectEvents()
        {
            System.Console.WriteLine("\n********* Directly using events **************\n");
            CarWithEvents carE = new CarWithEvents(10, 100, "Suzuki");
            carE.Exploded += OnCarEngineEvent;
            carE.AboutToBlow += OnCarEngineEvent;
            for (var i = 0; i < 10; i++)
            {
                carE.Accelerate(10);
            }
        }

        static private void EventHandlerTUsage()
        {
            System.Console.WriteLine("\n********* Directly using events with EventHandler<T> **************\n");
            MicrosoftRecommendedEventsWithArgs carE = new MicrosoftRecommendedEventsWithArgs(10, 500, "Lambo");
            carE.Exploded += (System.Object sender, CarEvents e) => System.Console.WriteLine(e.Message);
            carE.AboutToBlow += (System.Object sender, CarEvents e) => System.Console.WriteLine(e.Message);
            for (var i = 0; i < 10; i++)
            {
                carE.Accelerate(50);
            }
        }
    }
}
