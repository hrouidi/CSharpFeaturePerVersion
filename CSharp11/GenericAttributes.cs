using System;

namespace CSharp11
{
    public class GenericAttributes
    {

        [GenericAttribute<string>()]
        public string? Method() => default;
    }

    public class GenericAttribute<TItem> : Attribute { }
}