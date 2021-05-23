using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace CSharp5
{
    [TestFixture]
    public static class AsyncAwait
    {
        #region Contextual keywords

        [Test]
        public static void SomeMethodUsingAwaitAsyncAsVariablesNames()
        {
            int await = 1;
            Assert.AreEqual(1, await);
            string async = "ret";
            Assert.AreEqual("ret", async);
        }

        #endregion

        #region regular cases

        //Compute bound async method: tie up a thread 
        private static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1)
                              .All(i => n % i > 0)));
        }

        // IO bound async methods : dont tie up a thread
        private static Task<string> GetCreatedFileAsync(string DirPath)
        {
            var tcs = new TaskCompletionSource<string>();
            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = DirPath;
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
                watcher.Filter = "*.*";
                watcher.Created += (s, e) =>
                {
                    watcher.EnableRaisingEvents = false;
                    // setting task result
                    tcs.SetResult(e.FullPath);
                };
                //Start monitoring.
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                // setting task exception
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        #endregion

        #region Async methods arguments Check 

        [Test]
        public static void AsyncMethodThatChecksArguments_Tests()
        {
            // Diff behavior
            Assert.DoesNotThrow(() => AsyncMethodThatChecksArgumentsAsync(null));// Exception is within forget task
            Assert.Throws<ArgumentNullException>(() => AsyncMethodThatChecksArgumentsEagerly(null));
            // Same behavior
            Assert.ThrowsAsync<ArgumentNullException>(async () => await AsyncMethodThatChecksArgumentsAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await AsyncMethodThatChecksArgumentsEagerly(null));
        }

        // Arguments are checked asynchronously into the task ! 
        private static async Task<int> AsyncMethodThatChecksArgumentsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            await Task.Delay(1000);
            return 0;
        }
        // the best way : https://stackoverflow.com/questions/18656379/validate-parameters-in-async-method
        // C#7 : make the impl method  static local
        private static Task<int> AsyncMethodThatChecksArgumentsEagerly(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return AsyncMethodThatChecksArgumentsEagerly_Impl(name);
        }

        private static async Task<int> AsyncMethodThatChecksArgumentsEagerly_Impl(string name)
        {
            await Task.Delay(1000);
            return 0;
        }

        #region Comparaison with Iterator impl using Yield

        [Test]
        public static void IteratorMethodThatChecksArguments_Tests()
        {
            // Diff behavior
            Assert.DoesNotThrow(() => IteratorMethodThatChecksArguments(null));
            Assert.Throws<ArgumentNullException>(() => IteratorMethodThatChecksArgumentsEagerly(null));
            // Same behavior
            Assert.Throws<ArgumentNullException>(() => IteratorMethodThatChecksArguments(null).ToList());
            Assert.Throws<ArgumentNullException>(() => IteratorMethodThatChecksArgumentsEagerly(null).ToList());

        }

        private static IEnumerable<char> IteratorMethodThatChecksArguments(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            foreach (var ch in name)
                yield return ch;
        }

        private static IEnumerable<char> IteratorMethodThatChecksArgumentsEagerly(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return IteratorMethodThatChecksArgumentsEagerly_Impl(name);
        }

        private static IEnumerable<char> IteratorMethodThatChecksArgumentsEagerly_Impl(string name)
        {
            foreach (var ch in name)
                yield return ch;
        }
        #endregion

        #endregion

        #region Exceptions Tips

        // When method has void return, exceptions are posted to the synchronization
        //context(if present) and never to the caller
        // faults app execution and no way to catch it in try catch block !
        private static async void RunUnobservableException1()
        {
            // Throw null
            await Task.Delay(1000);
            throw new Exception("Will this be observed");
        }

        private static async void RunUnobservableException2()
        {
            throw new Exception("Will this be uncatched");
        }

        [Test]
        public static void UnobservableException_Test()
        {
            RunUnobservableException1();
            RunUnobservableException2();
        }

        // When method has void Task/Task<T>, exceptions are encapulated into task
        [Test]
        public static void SilentException_Test()
        {
            Assert.DoesNotThrow(() => RunSilentException());
            Assert.ThrowsAsync<Exception>(async () => await RunSilentException());
        }
        private static async Task<int> RunSilentException()
        {
            //throw null;
            await Task.Delay(1000);
            throw new Exception("Will this be ignored?");
        }
        //

        [Test]
        public static void SilentException1_Test()
        {
            Assert.DoesNotThrow(() => RunSilentException1());
            Assert.ThrowsAsync<Exception>(async () => await RunSilentException1());
        }
        private static async Task RunSilentException1()
        {
            // the key word async wrap the throw statement into a task 
            // so, exception is observable via Task, (with await for example...)
            throw new Exception("Will this be ignored?");
        }

        #endregion

        #region Async Tips

        [Test]
        public static void AsyncMethodWithOutOrRefParamsDoesNotCompile()
        {
            string code = @"private static async void AsyncRetOut(int x, ref int y, out int z)
                            {

                            }";

            var tmp = CSharpScript.Create(code);
            var dias = tmp.Compile();
            Assert.AreEqual(2, dias.Length);
            Assert.AreEqual("CS1988", dias[0].Id);
            Assert.AreEqual("CS1988", dias[1].Id);
        }

        // Async void
        private static async void AsyncVoid()
        {
            await Task.Delay(1);
        }

        // Async Task/Task<T>
        private static async Task<string> AsyncTask()
        {
            await Task.Delay(1);
            return "Done";
        }

        private static async ValueTask<string> AsyncValueTask()
        {
            await Task.Yield();
            return "Done";
        }

        // Async Lambda 
        public static async void lambda()
        {

            Func<Task<int>> unnamed = async () =>
            {
                await Task.Delay(1000);
                return 123;
            };
            int answer = await unnamed();
        }

        // async with Synchronous Completion : Good example
        static readonly Dictionary<string, string> _cache = new Dictionary<string, string>();
        public static async Task<string> GetWebPageAsync(string uri)
        {
            string html;
            if (_cache.TryGetValue(uri, out html))
                return html; // Synchronous path execution
            // Asynchronous path execution
            return _cache[uri] = await new WebClient().DownloadStringTaskAsync(uri);
        }
        // async with Synchronous Completion : Bad Exmaple : fully Synchronous
        // Notice that we have a compiler warning
        private static async Task<int> GetRandomNumberAsync()
        {
            Thread.Sleep(1000);
            return 0;
        }

        // Recursive async method
        private static async Task<int> RecFunction(int x)
        {
            if (x == 0)
                return 10;
            else
                return await RecFunction(x - 1);
        }


        #endregion

        #region Synchronization Context

        // Synchronization Context Auto capture
        public static async void UIEventHander()
        {
            int primescont = await GetPrimesCountAsync(0, 1000000);
            // UI upadtes on Synchronization Context ...
        }

        // Synchronization Context Configure capture
        // Usefull when develpoong Api methods to avoid context switch overhead
        public static async void ApiMethodImpl()
        {
            int primescont = await GetPrimesCountAsync(0, 1000000).ConfigureAwait(false);
            //NO Syncronization Context captured so continuation goes on Thread pool...
        }


        #endregion

        #region Await Tips

        //On Await into lock block
        //public static async void AwaitLock()
        //{
        //    lock(new object())
        //    {
        //        await Task.FromResult(1); 
        //    }
        //}
        [Test]
        public static void AwaitInLockBlockDoesNotCompile()
        {
            string code = @"using System.Threading.Tasks;
                            public static async void AwaitLock()
                            {
                                lock(new object())  
                                {  
                                    await Task.FromResult(1); 
                                }
                            }";

            var tmp = CSharpScript.Create(code);
            var dias = tmp.Compile();
            Assert.AreEqual(1, dias.Length);
            Assert.AreEqual("CS1996", dias[0].Id);
        }


        // Await already signaled tasks
        public static async void AwaitAlreadySignaledTasks()
        {
            await Task.FromResult(1);
        }

        // Await Never signaled tasks : very dangerous
        public static async void AwaitNeverSignaledTasks()
        {
            var ret = await new TaskCompletionSource<int>().Task;
            // Continuation never be executed 
        }

        //finally weakness : very dangerous
        public static async void AlexsMethod()
        {
            try
            {
                await new TaskCompletionSource<object>().Task;
            }
            finally
            {
                // Never happens
            }
        }

        // Await Task.Yield() : immediatly return to the caller (continuation is scheduled later )
        // is like Thread.Sleep(0) when scheduled on ThreadPool
        // usefull for micro optimization when synchrounous initialization time is an overhead
        public static async Task<int> AwaitYield()
        {
            await Task.Yield();
            // some time consume work...
            return 1;
        }

        // await custom awaitable object (not task)
        // Any objet (class/struct) that have Getwaiter() methdo as instance method or extension method also
        // But no auto context capture (in need, you should implement context capture into custom Awaiters)
        public static async void AwaitingCustomAwaitbleMethod()
        {
            int ret = await new CustomAwaitble();
        }

        public class CustomAwaitble
        {
            public CustomAwaiter GetAwaiter()
            {
                return new CustomAwaiter();
            }
        }
        // CustomAwaiter must 
        //1.implement INotifyCompletion 
        //2.have IsCompleted propery for synchrounous check optimisation
        //3.have GetGetResult() methods(return type can be void or any type as Task and Task<T>)
        public struct CustomAwaiter : INotifyCompletion
        {
            public bool IsCompleted
            {
                get { return true; }
            }

            public int GetResult()
            {
                return 0;
            }

            public void OnCompleted(Action continuation)
            {
                continuation();
            }
        }

        // await existing type with extension methods
        public static async void AwaitingExistingTypeMethod()
        {
            await "c'est pas vrai";
        }
        // cascading await statments
        public static async void AwaitAsCascade()
        {
            await Task.FromResult(1);
            await Task.FromException(null);
            await Task.FromCanceled(new CancellationToken());
        }
        // chaining await statments
        static async Task<int> Delay1() { await Task.Delay(1000); return 1; }
        static async Task<int> Delay2() { await Task.Delay(2000); return 2; }
        static async Task<int> Delay3() { await Task.Delay(3000); return 3; }
        public static async void AwaitAwaitMethod()
        {
            int ret = await await Task.WhenAny(Delay1(), Delay2(), Delay3());
            // OR
            Task<int> tmp = await Task.WhenAny(Delay1(), Delay2(), Delay3());
            ret = await tmp;
        }

        // Je me fais plaise ^^ avec mon MetaAwaiter
        // et çà compile lol
        public static async void Await________AwaitMethod()
        {
            var ret = await await await await await await await await 3;
        }

        #endregion
    }

    public static class AwaiterExtensions
    {
        public static TaskAwaiter GetAwaiter(this string instance)
        {
            return new TaskAwaiter();
        }

        public static MetaAwaiter GetAwaiter(this int instance)
        {
            return new MetaAwaiter();
        }
    }

    public class MetaAwaiter : INotifyCompletion
    {
        public bool IsCompleted
        {
            get { return true; }
        }

        public Task<int> GetResult()
        {
            return Task.FromResult(0);
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }
}
