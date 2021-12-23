using System;
using NUnit.Framework;

namespace CSharp9
{
    public class StaticLambda
    {
        private readonly Func<double, double> _square = static x => x * x;

        [Test]
        public void Test()
        {
            Assert.AreEqual(4, _square(2));
        }
    }
}
