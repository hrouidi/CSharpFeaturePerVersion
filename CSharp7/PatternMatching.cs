using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    class PatternMatching
    {

        // with is
        public static int SampleIs(IEnumerable<object> values)
        {
            var sum = 0;
            foreach (var item in values)
            {
                if (item is int val)
                    sum += val;
                else if (item is IEnumerable<object> subList)
                    sum += SampleIs(subList);
            }
            return sum;
        }

        // with switch
        public static int SampleSwitch(IEnumerable<object> values)
        {
            var sum = 0;
            foreach (var item in values)
            {
                switch (item)
                {
                    case 0:
                        break;
                    case int val:
                        sum += val;
                        break;
                    case IEnumerable<object> subList when subList.Any():
                        sum += SampleSwitch(subList);
                        break;
                    case IEnumerable<object> subList:
                        break;
                    case null:
                        break;
                    default:
                        throw new InvalidOperationException("unknown item type");
                }
            }
            return sum;
        }
    }
}
