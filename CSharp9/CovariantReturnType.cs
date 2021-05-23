using System;
using NUnit.Framework;

namespace CSharp9
{
    public class ClassBase 
    {
        public virtual object Clone() => this;
    }
    public class CovariantReturnType : ClassBase
    {
        public override CovariantReturnType Clone() => this;
    }

    public class FixtureTest
    {
        [Test]
        public void BaseMethodOverride_Test()
        {
            var returnValue1 = new CovariantReturnType().Clone();
            var returnValue2 = new ClassBase().Clone();
            Assert.IsInstanceOf<CovariantReturnType>(returnValue1);
            Assert.IsInstanceOf<object>(returnValue2);
        }

        abstract class Cloneable
        {
            public abstract Cloneable Clone();
        }

        class Digit : Cloneable
        {
            public override Digit Clone()
            {
                return this;
            }
        }

        [Test]
        public void AbstractMethodOverride_Test()
        {
            var returnValue1 = new Digit().Clone();
            Assert.IsInstanceOf<Digit>(returnValue1);
        }
    }
}