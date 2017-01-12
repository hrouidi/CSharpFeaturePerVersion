using System.Collections.Generic;

namespace CSharp6.Feature
{
    class IndexInitializers
    {
        private Dictionary<int, string> Books = new Dictionary<int, string>()
        {
            [1] = "ASP.net",
            [2] = "C#",
            [3] = "ASP.net MVC5"
        };

        //Advantages

        //– You can create and initialize objects with indexes at the same time.
        //– Initializing Dictionary is very easy with this new feature
        //– Any object that has an indexed getter or setter can be used with this syntax
    }
}
