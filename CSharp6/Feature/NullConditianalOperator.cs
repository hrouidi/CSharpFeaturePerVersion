using System.Text;

namespace CSharp6.Feature
{
    class NullConditianalOperator
    {
        public void Do(StringBuilder sb)
        {
            //Elvis operator: I Like :)
            string s = sb?.ToString(); // No error; s instead evaluates to null
            // <==>
            string ss = (sb == null ? null : sb.ToString());


            var ret = sb?.Append("ss")?.ToString();
        }
    }
}
