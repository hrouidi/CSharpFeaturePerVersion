using static System.Console;

namespace CSharp6.Feature
{
    class UsingStatic
    {
        public void Do()
        {
            //Console.WriteLine("Hello World!");// Does'nt compile

            // with using static System.Console;
            WriteLine("Hello World!");
        }
    }
}
