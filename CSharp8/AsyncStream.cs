using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp08
{
    internal class AsyncStreamTests
    {
        [Test]
        public async Task Test1()
        {
            await foreach (var item in GenerateSequenceAsync())
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public async Task Test2()
        {
            await foreach (var item in GenerateSequenceAsync().ConfigureAwait(false))
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public async Task Test3()
        {
            CancellationTokenSource cts = new();
            await foreach (var item in GenerateSequenceAsync().WithCancellation(cts.Token))
            {
                Console.WriteLine(item);
                cts.Cancel();
            }
        }


        [Test]
        public async Task Test4()
        {
            CancellationTokenSource cts = new();
            await foreach (var item in 1..10)
            {
                Console.WriteLine(item);
                cts.Cancel();
            }
        }

        private static async IAsyncEnumerable<int> GenerateSequenceAsync(CancellationToken cancellation = default)
        {
            foreach (int i in Enumerable.Range(0, 10))
            {
                await Task.Delay(i, cancellation);
                yield return i;
            }
        }
    }

    public static class RangeExtensions
    {
        public static IAsyncEnumerator<int> GetAsyncEnumerator(this Range range, CancellationToken cancellation = default)
        {
            return new CustomAsyncEnumerator();
        }

        public class CustomAsyncEnumerator : IAsyncEnumerator<int>
        {
            public async ValueTask DisposeAsync()
            {
                // TODO release managed resources here
            }

            public async ValueTask<bool> MoveNextAsync()
            {
                await Task.Delay(1);
                Current++;
                return Current < 10;
            }

            public int Current { get; private set; }
        }
    }
}
