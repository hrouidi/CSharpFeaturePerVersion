using System.Diagnostics.CodeAnalysis;

namespace CSharp9
{
    public class LocalFunctionAttribute
    {

        public int MainFunction()
        {
            [ExcludeFromCodeCoverage]
            int LocalFunction(string parameter)
            {
                return 0;
            }

            return LocalFunction("Ok");
        }
    }
}
