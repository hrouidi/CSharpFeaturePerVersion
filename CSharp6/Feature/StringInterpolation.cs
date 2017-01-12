using System;

namespace CSharp6.Feature
{
    class StringInterpolation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Do(string[] args)
        {
            var cus = new StringInterpolation();
            cus.FirstName = "Rouid";
            cus.LastName = "Houssam";

            Console.WriteLine($"{FirstName} {LastName} is my name!");
        }
    }
}
