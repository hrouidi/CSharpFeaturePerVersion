using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp9
{
    public class RecordType
    {
        public record Person(string FirtName, string LastName);

        [Test]
        public static void RecordType_Test()
        {
            Person me = new("Houssam", "Rouidi");
            Assert.AreEqual(("Houssam", "Rouidi"), (me.FirtName, me.LastName));
            var alterEgo = new Person("Houssam", "Rouidi");
            // reference equality
            Assert.AreNotSame(me, alterEgo);
            Assert.IsFalse(ReferenceEquals(me, alterEgo));
            //structured equality
            Assert.IsTrue(me.Equals(alterEgo));
            Assert.AreEqual(me.GetHashCode(), alterEgo.GetHashCode());
            var (f, l) = me;
            Assert.AreEqual("Houssam", f);
            Assert.AreEqual("Rouidi", l);
            //me.FirtName = "kk"; Does'not compile
            var you = me with { FirtName = "You" };
            Assert.AreEqual("You", you.FirtName);
            Assert.AreEqual("Rouidi", you.LastName);
        }
        private string Get(string name)
        {
            if (name is not null)
                return name;
            return string.Empty;
        }
    }
}
