using NUnit.Framework;
using System;

namespace CSharp12
{
    internal class InlineArraysTest
    {
        [System.Runtime.CompilerServices.InlineArray(10)]
        public struct Buffer
        {
            private int _element0;
        }

        [Test]
        public void Test1()
        {
            var buffer = new Buffer();
            for (int i = 0; i < 10; i++)
            {
                buffer[i] = i;
            }

            foreach (var i in buffer)
            {
                Console.WriteLine(i);
            }
        }
    }
}
