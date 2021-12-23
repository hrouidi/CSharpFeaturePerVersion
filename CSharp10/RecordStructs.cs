using NUnit.Framework;

namespace CSharp10
{
    public class Tests
    {
        public record struct RecordStruct
        {
            public int Id { get; init; }

            public string Name { get; init; }
        }

        public struct SimpleStruct
        {
            public int Id { get; init; }

            public string Name { get; init; }
        }

        [Test]
        public void Test()
        {
            RecordStruct obj1 = new() { Id = 1, Name = "name1" };
            RecordStruct obj2 = new() { Id = 1, Name = "name1" };


            SimpleStruct tmp1 = new() { Id = 1, Name = "de" };
            SimpleStruct tmp2 = tmp1 with { Name = "tmp2" };
        }
    }
}