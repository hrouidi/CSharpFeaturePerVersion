using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp5
{
    public static class AsyncAwaitInDepth
    {
        public static async void RunSample()
        {
            var ret = await await await await await await await await 123;

            //int primescont = await GetPrimesCountAsync(0, 1000000);
            //string createdFilePath = await GetCreatedFileAsync(Directory.GetCurrentDirectory());

            #region  Exceptions

            RunSilentException();// innobservable exception 
            RunSilentException1();// innobservable exception 
            MethodThatCheckArguments(string.Empty);// but no ArgumentNullException
            //can catch ths exceptions
            try
            {
                await RunSilentException(); // observable exception 
                //await RunSilentException1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // cannot catch this exeption on Apps with synchronization context
            try
            {
                RunUnhandledException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // cannot catch this exeption on Apps with synchronization context
            try
            {
                RunUncatchedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

        }

        #region Normal case

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

        #region Exceptions Tips

        // Common error: arguments check, it s like iterator yield methods
        // C#7 loacl method can help with :)
        private static async Task<int> MethodThatCheckArguments(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            await Task.Delay(1000);
            return 0;
        }

        // When method has void return, exceptions are posted to the synchronization
        //context(if present) and never to the caller
        // faults app execution and no way to catch it in try catch block !
        private static async void RunUnhandledException()
        {
            // Throw null
            await Task.Delay(1000);
            throw new Exception("Will this be observed");
        }
        private static async void RunUncatchedException()
        {
            throw new Exception("Will this be uncatched");
        }

        // When method has void Task/Task<T>, exceptions are encapulated into task
        private static async Task<int> RunSilentException()
        {
            //throw null;
            await Task.Delay(1000);
            throw new Exception("Will this be ignored?");
        }
        //
        private static async Task RunSilentException1()
        {
            // the key word async wrap the throw statement into a task 
            // so exception is observable via Task, (with await for exmaple...)
            throw new Exception("Will this be ignored?");
        }

        #endregion

        #region Async Tips

        // No async on methods that takes out/ret parameters
        //private static async void AsyncRetOut(int x,ref int y,out int z)
        //{

        //}

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
            public bool IsCompleted => true;

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
        public bool IsCompleted => true;

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
