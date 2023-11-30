using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp12
{
    internal class DefaultLambdaParametersTests
    {
        Func<string, int, bool> isTooLong1 = (s, x) => s.Length > x;
        Func<string, int, bool> isTooLong2 = (string s, int x) => s.Length > x;
        

        [Test]
        public void Test1()
        {
            var isTooLong3 = (string s, int x = 12) => s.Length > x;

            bool ret1 = isTooLong1("Word", 12);
            bool ret2 = isTooLong2("Word", 12);

            bool ret3 = isTooLong3("Word");
        }
    }
}
