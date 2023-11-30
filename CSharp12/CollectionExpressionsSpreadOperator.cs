using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CSharp12
{
    public class CollectionExpressionsSpreadOperatorTests
    {
        [Test]
        public void Test1()
        {
            // Collection Expressions
            int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

            List<string> b = ["one", "two", "three"];

            Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

            int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

            int[] row0 = [1, 2, 3];
            int[] row1 = [4, 5, 6];
            int[] row2 = [7, 8, 9];
            int[][] twoDFromVariables = [row0, row1, row2];

            //Spread operator

            //# from native types
            int[] single = [.. row0, .. row1, .. row2];
            //# from custom types via GetEnumerator extension method
            Spreadable speadable = new();

            int[] generated = [.. speadable];


            // Collection builder
            LineBuffer line = ['H', 'e', 'l', 'l', 'o'];
        }
        
    }

    public class Spreadable
    {
        private readonly char[] _buffer;
    }

    public static class SpreadableExtensions
    {
        public static IEnumerator<int> GetEnumerator(this Spreadable self) => Enumerable.Range(0, 10).GetEnumerator();
    }

    [CollectionBuilder(typeof(LineBufferBuilder), nameof(LineBufferBuilder.Create))]
    public class LineBuffer : IEnumerable<char>
    {
        private readonly char[] _buffer = new char[80];

        public LineBuffer(ReadOnlySpan<char> buffer)
        {
            int number = (_buffer.Length < buffer.Length) ? _buffer.Length : buffer.Length;
            for (int i = 0; i < number; i++)
            {
                _buffer[i] = buffer[i];
            }
        }


        public IEnumerator<char> GetEnumerator() => _buffer.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _buffer.GetEnumerator();
    }

    public static class LineBufferBuilder
    {
        public static LineBuffer Create(ReadOnlySpan<char> values) => new LineBuffer(values);
    }
}