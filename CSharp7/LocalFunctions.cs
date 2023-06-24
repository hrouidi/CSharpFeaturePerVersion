using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    public class LocalFunctions
    {
        // order definition of local method doesn't matter
        public static int Sum(int x, int y, int z)
        {
            int localSum(int xx, int yy)
            {
                return xx + yy + z;
            }
            return localSum(x, y);
        }
        // Many local functions
        public static int Sum1(int x, int y, int z)
        {
            int localSum1()
            {
                return x + y;
            }

            int localSum2()
            {
                return z + localSum1();
            }
            return localSum1() + localSum2();
        }

        //Expression body local function
        public static int Sum2(int x, int y, int z)
        {
            int localSum(int xx, int yy) => xx + yy + z;

            return localSum(x, y);
        }

        //Nested local functions
        public static int Method1(int x, int y, int z)
        {
            int localVar0 = 1;
            int local1(int xx, int yy)
            {
                int localVar1 = 3;
                //nested local function can capture any local/parameter variable from higher level
                int local2(int xxx, int yyy)
                {
                    int local3(int xxxx, int yyyy)
                    {
                        return x + xx + xxx + localVar1 + localVar0;
                    }
                    return local3(x + xx, xxx) + localVar1 + localVar0;

                }

                return local2(localVar0, z);
            }
            return local1(x, y);
        }

        // recurcive local functions


        #region Usefull case
        //on iterator method
        public static IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImplementation();
            // capturing local variable (start) and (end)
            IEnumerable<char> alphabetSubsetImplementation()
            {
                for (var c = start; c < end; c++)
                    yield return c;
            }
        }
        // on Async method
        public Task<string> PerformLongRunningWork(string address, int index, string name)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(message: "An address is required", paramName: nameof(address));
            if (index < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "The index must be non-negative");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(message: "You must supply a name", paramName: nameof(name));

            return LongRunningWorkImplementation();
            // capturing local variables
            async Task<string> LongRunningWorkImplementation()
            {
                var interimResult = await Task.FromResult(index);
                return $"The results are {interimResult} . Enjoy.";
            }
        }

        #endregion
    }
}
