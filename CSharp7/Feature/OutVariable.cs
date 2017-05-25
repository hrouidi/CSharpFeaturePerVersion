using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7.Feature
{
    class OutVariable
    {
        public static void Sample()
        {
            // With  Try Pattern
            if (int.TryParse("123", out int result01))
                Console.WriteLine(result01);

            // With  var 
            if (int.TryParse("123", out var result02))
                Console.WriteLine(result02);

            //Mixed
            int ret1;
            if(TryParse("1", "2", out ret1, out var ret2))
                Console.WriteLine($"{ret1} : {ret2}");
        }

        private static bool TryParse(string s1,string s2,out int num1,out int num2)
        {
            var ret = int.TryParse(s1, out num1);
            ret =  int.TryParse(s2, out num2) && ret;
            return ret ;
        }
    }
}
