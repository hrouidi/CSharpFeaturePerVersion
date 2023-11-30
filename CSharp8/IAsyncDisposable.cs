using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CSharp08
{
    internal class IAsyncDisposableTests
    {
        public class Dispo : IAsyncDisposable
        {
            public async ValueTask DisposeAsync()
            {
                await Task.Delay(10);
            }
        }

        [Test]
        public async Task Test1()
        {
            await using var instance = new Dispo().ConfigureAwait(false);
        }
    }
}
