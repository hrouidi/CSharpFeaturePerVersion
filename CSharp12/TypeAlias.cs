using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

using Point = (int x, int y);
using PointEx = (int x, int y);
using DataSource = System.Collections.Generic.IReadOnlyDictionary<(int x, int y), System.Linq.ILookup<int, string>>;

namespace CSharp12
{
    internal class TypeAliasTest
    {
        [Test]
        public void Test1()
        {
            Point p = new Point(1, 3);
            p.x = 1;
            PointEx pEx = new PointEx(1, 3);

            DataSource ds = new Dictionary<Point, ILookup<int, string>>();
        }
    }
}
