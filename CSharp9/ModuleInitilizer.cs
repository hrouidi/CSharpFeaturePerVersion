using System.Runtime.CompilerServices;

namespace CSharp9
{
    public class ModuleInit
    {
        [ModuleInitializer]
        public static void InitializeModule1()
        {

        }

        [ModuleInitializer]
        public static void InitializeModule2()
        {

        }
    }
}
