using System;

namespace CSharp9
{
    public class StaticLambda
    {
        private Func<double, double> _square = static x => x * x;


        //public double Method()
        //{
        //    int y = 5;
        //    Func<int, double> func = static x => x * y;
        //    return func(10);
        //}
    }
}
