using System;
using System.Threading;

namespace ConsoleApp1
{
    static class Aspect
    {
        /// <summary>
        /// retry to run when fail.
        /// </summary>
        public static void RetryAspect(int retryTimes, int milliSeconds, Action action)
        {
            do
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (retryTimes <= 0) throw;
                    Console.WriteLine("Exception {0} → retry", ex.Message);
                    SpinWait.SpinUntil(() => false, milliSeconds); // waiting milliSeconds
                }
            } while (retryTimes-- > 0);
        }

        /// <summary>
        /// ignore fail.
        /// </summary>
        public static void IgnoreAspect(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0} → ignore", ex.Message);
            }
        }

        /// <summary>
        /// tracing
        /// </summary>
        public static void LogAspect(Action action)
        {
            Console.WriteLine("BEGIN");
            action();
            Console.WriteLine("END");
        }

        /// <summary>
        /// run while true.
        /// </summary>
        public static void WhileAspect(bool whileTrue, Action action)
        {
            if (whileTrue)
            {
                Console.WriteLine("while true → go ");
                action();
            }
            else
            {
                Console.WriteLine("while false → leave");
            }
        }
    }
}