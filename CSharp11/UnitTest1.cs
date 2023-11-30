using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp11
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (true)
            {
                bool ret = await timer.WaitForNextTickAsync();
                await Task.Delay(10_000);
            }
        }
    }
}