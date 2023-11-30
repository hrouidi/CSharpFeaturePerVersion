using NUnit.Framework;
using System;

namespace CSharp12
{
    internal class RefReadOnlyTests
    {
        class AClass
        {
            public static string AMethod(ref readonly int i) => i.ToString();
        }


        [Test]
        public void Test1()
        {
            int parameter = 5;
            Console.WriteLine(AClass.AMethod(ref parameter));
            Console.WriteLine(AClass.AMethod(in parameter));
            Console.WriteLine(AClass.AMethod(parameter)); //warning
            //Console.WriteLine(AClass.AMethod(out parameter));//Error
        }

    }
}
