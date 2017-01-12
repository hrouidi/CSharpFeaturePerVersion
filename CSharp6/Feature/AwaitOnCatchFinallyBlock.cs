using System;
using System.Net.Http;

namespace CSharp6.Feature
{
    class AwaitOnCatchFinallyBlock
    {
        public async void Do()
        {
            HttpClient client = new HttpClient();
            try
            {
                var result = await client.GetStringAsync("http://www.bing.com");
                Console.WriteLine(result);       // You could do this.
            }
            catch (Exception e)
            {
                var result = await client.GetStringAsync("http://www.google.com");
                Console.WriteLine(result);         // Now you can do this …
            }
            finally
            {
                var result = await client.GetStringAsync("http://www.yahoo.com");
                Console.WriteLine(result); // … and this.
            }
        }
    }
}
