using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.ValueTuple;


namespace CSharp7.Feature
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
    public static class  PointExtension
    {
        public static void Deconstruct(this Point point, out int x, out int y)
        {
            x = point.X;
            y = point.Y;
        }
    }

    class Tuples
    {
        public Tuples()
        {
            //nammed items:
            (string name, int value) namedLetters = ("one", 1);

            //Nested tuples
            var tu = (1, (2, (3, 4)), 5, 6, 7, 8, 9, 10, 11, 12, 13);
            (int first, (int, int) rest) seb = tu.Item2;
            (int first, (int, int) rest) ret2 = tu.Item2;
            var matrix = ((1, 2, 3), (1, 2, 3), (1, 2, 3));

           
            //Deconstruction
            var p = new Point(0, 1);

            //explicit type
            (int x , int y) = p;
            if (x == y)
                Console.WriteLine($"{x} = {y}");
            // infereded type
            var( xx,  yy) = p;

            // Function that return a tuple
            // with var
            var ret0 = GetBornes(Enumerable.Range(0, 10));
            if (ret0.Item1 >= ret0.Item2)
                Console.WriteLine("ok");
            //explicit type 
            (int , int ) ret1 = GetBornes(Enumerable.Range(0, 10));
            if (ret1.Item1 >= ret1.Item2)
                Console.WriteLine("ok");
            // renaming item tuples
            (int max, int min ) ret3 = GetBornes(Enumerable.Range(0, 10));
            if (ret3.max >= ret3.min)
                Console.WriteLine("ok");
            // reuse existing variables
            int localMax = 0;
            int localMin = 0;
            (localMax, localMin) = GetBornes(Enumerable.Range(0, 10));
            if (localMax == 0)
                Console.WriteLine("ok");
            if (localMin == 0)
                Console.WriteLine("ok");


            // infinite tuple items
            var veryBigTuple = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,"def",10.23,DateTime.Now);

            // convertion to legacy tuple type
            Tuple<int, int,string> eddd = (1,2,"done").ToTuple();
            //Doesn't compile !
            //var legacyTuple = veryBigTuple.ToTuple();
            
            // Usefull usage :

            //1.Multiple affectation
            int a = 0;
            int b = 0;
            (a, b) = (a + b, a - b);
            (a, b) = (10, 32);
            Assert.AreEqual(10, a);
            Assert.AreEqual(32, b);
            //2.elegant  Permutation
            (a, b) = (b, a);
            Assert.AreEqual(32, a);
            Assert.AreEqual(10, b);

        }

       private static (int Max, int Min) GetBornes(IEnumerable<int> numbers)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var n in numbers)
            {
                min = n < min ? n : min;
                max = n > max ? n : max;
            }
            return (max, min);
        }
    }
}
