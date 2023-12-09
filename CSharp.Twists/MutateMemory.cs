using NUnit.Framework;
using System.Runtime.InteropServices;
using System;

namespace CSharp.Twists
{
    public class MutateMemory
    {
        [Test]
        public static void MutateString()
        {
            const string text = "Cat";
            Span<char> stringAsSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(text.AsSpan()), text.Length);
            stringAsSpan[0] = 'R';
            Assert.That(text, Is.EqualTo("Rat"));
        }

        [Test]
        public static void MutateInteger()
        {
            int constant = Int32.MaxValue;
            Span<byte> intBytesAsSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref constant, 1));
            intBytesAsSpan[0] = 0xff;
            intBytesAsSpan[1] = 0;
            intBytesAsSpan[2] = 0;
            intBytesAsSpan[3] = 0;
            Assert.That(constant, Is.EqualTo(0xff));
        }
    }
}