namespace DelegateEventsLambdas
{
    // A simple delegate
    public delegate int BinaryOp(int a, int b);
    internal static class SimpleMath
    {
        public static int Add (int x, int y) => x+y;

        public static int Subtratct (int x, int y) => x-y;

        public static int Multiply (int x, int y) => x*y;

        public static int Divide (int x, int y) => x/y;
    }
}