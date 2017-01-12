using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ValueTuple;

namespace CSharp7.Feature
{
    class Tuples
    {
        public class Point
        {
            public Point(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            public double X { get; }
            public double Y { get; }

            public void Deconstruct(out double x, out double y)
            {
                x = this.X;
                y = this.Y;
            }
        }

        public const int Sixteen = 0b0001_0000;
        public const long BillionsAndBillions0 = 100_000__000_000;
        public const long BillionsAndBillions1 = 100_000__000_000;
        public const long BillionsAndBillions2 = 100_000____000_000;

        string name;

        public string Name
        {
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value), "New name must not be null");
        }

        public Tuples()
        {
            (string Alpha, string Beta) namedLetters = ("a", "b");
            var p = new Point(3.14, 2.71);
            (double X, double Y) = p;
            (double horizontalDistance, double verticalDistance) = p;
        }

        private static (int Max, int Min) Range(IEnumerable<int> numbers)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var n in numbers)
            {
                min = (n < min) ? n : min;
                max = (n > max) ? n : max;
            }
            return (max, min);
        }

        public async ValueTask<int> Func()
        {
            await Task.Delay(100);
            return 5;
        }

        public Task<string> PerformLongRunningWork(string address, int index, string name)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(message: "An address is required", paramName: nameof(address));
            if (index < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "The index must be non-negative");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(message: "You must supply a name", paramName: nameof(name));

            return longRunningWorkImplementation();

            async Task<string> longRunningWorkImplementation()
            {
                var interimResult = await Task.FromResult(index);
                return $"The results are {interimResult} . Enjoy.";
            }
        }
    }
}
