using System;
using NUnit.Framework;

namespace CSharp11
{
    public class Utf8Literals
    {
        [Test]
        public void Test1()
        {
            ReadOnlySpan<byte> bytes = "Hello!"u8;
        }
    }
}