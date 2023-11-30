using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp11
{
    public class RawStringLiteralsTests
    {
        [Test]
        public async Task Test1()
        {
            string longMessage = """
                                 This is a long message.
                                 It has several lines.
                                     Some are indented
                                             more than others.
                                 Some should start at the first column.
                                 Some have "quoted text" in them.
                                 """;

            string longMessage2 = """"
                                 This ""is a """long message.
                                 It has several lines.
                                     Some are indented
                                             more than others.
                                 Some should start at the first column.
                                 Some have "quoted text" in them.
                                 """";

            var location = $$"""
                             You are at {{{10}}, {{25}}}
                             """;
        }
    }
}