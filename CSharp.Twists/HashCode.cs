using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharp.Twists
{
    public class HashCode
    {
        [Test]
        public static void Test()
        {
            Key key = new() { Part1 = 1, Part2 = 2 };
            Dictionary<Key, string> dictionary = new() { { key, "value1" } };

            Assert.That(dictionary.ContainsKey(key), Is.EqualTo(true));
            key.Part2 = 12;
            Assert.That(dictionary.ContainsKey(key), Is.EqualTo(false));
        }
    }

    public sealed class Key : IEquatable<Key>
    {
        public int Part1 { get; set; }
        public int Part2 { get; set; }

        public bool Equals(Key? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Part1 == other.Part1 && Part2 == other.Part2;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Key other && Equals(other);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Part1, Part2);
        }
    }
}