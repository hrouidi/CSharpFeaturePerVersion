using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9
{
    public class InitOnlyProperties
    {
        //public class Doto(string name);
        public class Person
        {
            private readonly string _firstName;
            private readonly string _lastName;

            public string FirstName
            {
                get => _firstName;
                init => _firstName = value ?? throw new ArgumentNullException(nameof(FirstName));
            }
            public string LastName
            {
                get => _lastName;
                init => _lastName = value ?? throw new ArgumentNullException(nameof(LastName));
            }
        }
        
        //public data class Personz(string FirstName, string LastName)
        [Test]
        public static void InitOnlyProperties_Test()
        {
            var person = new Person { FirstName = "Houssam", LastName = "Rouidi" };
            Assert.AreEqual(("Houssam", "Rouidi"), (person.FirstName, person.LastName));
            //person.LastName = "notAllowed"; // Does'nt complie
        }
    }
}
