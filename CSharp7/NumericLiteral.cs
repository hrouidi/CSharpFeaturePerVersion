using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    class NumericLiteral
    {
        //Intergral

        public const byte num00 = 0b0001_0000;
        public const ushort num02 = 1000_0;
        public const uint num03 = 1__000_000U;
        public const ulong num04 = 1_000____000_000UL;

        public const sbyte num10 = 0b0001_0000;
        public const short num12 = 1000_0;
        public const int num13 = 1__000_000;
        public const long num14 = 1_000____000_000L;

        //Real 
        public const float num2 = 100_000.000_001F;
        public const decimal num3 = 100_000.223_222M;
        public const double num4 = 100_000____000_000D;

        // still valide !!
        public const int xx = 1__0_0__0_0__0___0;
    }
}
