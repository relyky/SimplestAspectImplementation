using System;
using static ConsoleApp1.Aspect;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int runTimes = 0;

            WhileAspect(true, () =>
            LogAspect(() =>
            IgnoreAspect(() =>
            RetryAspect(2, 3000, () =>
            {
                Console.WriteLine($"times: {++runTimes}");
                Console.WriteLine("step 1");
                Console.WriteLine("step 2");
                if (runTimes < 3)
                    throw new ApplicationException("例外測試");
                Console.WriteLine("step 3");
            }))));
        }

    }
}
