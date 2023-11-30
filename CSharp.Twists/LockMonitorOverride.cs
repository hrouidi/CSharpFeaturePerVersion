using NUnit.Framework;

namespace CSharp.Twists
{
    public class MonitorOverrideTests
    {
        [Test]
        public void Test1()
        {
            object sync = new object();
            lock (sync)
            {
                
            }
        }
    }
}


namespace System.Threading
{
    public static class Monitor
    {
        public static void Enter(object obj, ref bool lockTaken)
        {
            Console.WriteLine("Entering the lock...");
            lockTaken = true;
        }

        public static void Exit(object obj)
        {
            Console.WriteLine("Releasing the lock...");
        }
    }
}