using System;
using System.Collections.Generic;
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

    public interface ITokenizableDefinition
    {
        ITokenizableDefinition ApplyTextSubstitutions(IEnumerable<string> textSub);
    }

    public sealed record SqliteDefinition : ITokenizableDefinition
    {
        public ITokenizableDefinition ApplyTextSubstitutions(IEnumerable<string> textSub)
        {
            return this;
        }
    }

    interface A
    {
        object Method1();
    }

    class B : A
    {
        public string Method1() => throw new NotImplementedException();
        object A.Method1() => Method1();
    }

    public class FixtureTest
    {
        [Test]
        public void BaseMethodOverride_Test()
        {
            CovariantReturnType returnValue1 = new CovariantReturnType().Clone();
            object returnValue2 = new ClassBase().Clone();
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
            Digit returnValue1 = new Digit().Clone();
            Assert.IsInstanceOf<Digit>(returnValue1);
        }
    }
}