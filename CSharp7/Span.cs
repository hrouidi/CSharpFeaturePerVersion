using System;
using NUnit.Framework;
using System.Runtime.InteropServices;

namespace CSharp7
{
	public class Span
	{
		[Test]
		public static void MutateString()
		{
			const string text = "Hello ! ";
			Assert.That(text, Is.EqualTo("Hello ! "));
			Method1(text, "Good bye");
			Assert.That(text, Is.EqualTo("Good bye"));

		}

		private static void Method1(string text, string newContent)
		{
			Span<char> stringAsSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(text.AsSpan()), text.Length);
			for (int i = 0; i < stringAsSpan.Length; i++)
				stringAsSpan[i] = newContent[i];
		}


		[Test]
		public static void Ex()
		{
			const string text = "Cat";
			Span<char> stringAsSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(text.AsSpan()), text.Length);
			stringAsSpan[0] = 'R';
			Console.WriteLine(text);

		}
	}
}