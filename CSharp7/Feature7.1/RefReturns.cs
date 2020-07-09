using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7.Feature
{
    class RefReturns
    {

#if DoesNotCompile

        public static ref int ReturnLocalValue(int[,] matrix, Func<int, bool> predicate)
        {
            int local = 2;
            return ref ++local;
        }

        public static ref int ReturnValueWithoutRef(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return  matrix[i, j];
            throw new InvalidOperationException("Not found");
        }
#endif
        // notice ref on return method type and on return statement method body

        // Ref reference
        private static ref string ReturnRefReference(string[,] matrix, Func<string, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found");
        }

        //ref Value 
        private static ref int ReturnValueByRef(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found");
        }

        public static void RunValueCall()
        {
            //
            int[,] matrix = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var valItem = ReturnValueByRef(matrix, (val) => val == 3);
            Console.WriteLine(valItem);
            valItem = 24;
            Console.WriteLine(matrix[0, 2]);// print 3 instead of 24;
        }

        public static void RunByRefCall()
        {
            //
            int[,] matrix = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            // use of ref local to capture return value by ref
            ref var valItem = ref ReturnValueByRef(matrix, (val) => val == 3);
            Console.WriteLine(valItem);
            valItem = 24;
            Console.WriteLine(matrix[0, 2]);// print  24;
        }

        public static void RunReferenceCall()
        {
            //
            string[,] matrix = new[,] { { "1", "2", "3" }, { "4", "5", "6" } };
            // use of ref local to capture return value by ref
            var valItem = ReturnRefReference(matrix, (val) => val == "3");
            Console.WriteLine(valItem);
            valItem = "24";
            Console.WriteLine(matrix[0, 2]);// print 3 instead of 24;
        }

        public static void RunByRefReferenceCall()
        {
            //
            string[,] matrix = new[,] { { "1", "2", "3" }, { "4", "5", "6" } };
            // use of ref local to capture return value by ref
            ref var valItem = ref ReturnRefReference(matrix, (val) => val == "3");
            Console.WriteLine(valItem);
            valItem = "24";
            Console.WriteLine(matrix[0, 2]);// print 24;
        }
    }
}
