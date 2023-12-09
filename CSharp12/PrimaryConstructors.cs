using NUnit.Framework;
using System;

namespace CSharp12
{
    public class PrimaryConstructorsTests
    {
       

        public struct Distance(double dx, double dy)
        {
            public readonly double Magnitude => Math.Sqrt(dx * dx + dy * dy);
            public readonly double Direction => Math.Atan2(dy, dx);

            public void Translate(double deltaX, double deltaY)
            {
                dx += deltaX;
                dy += deltaY;
            }

            public Distance() : this(0, 0) { }
        }

        [Test]
        public void Test1()
        {
            Distance distance = new(double.Epsilon, double.Tau);
            distance.Translate(1.0,2.0);
        }
    }
}