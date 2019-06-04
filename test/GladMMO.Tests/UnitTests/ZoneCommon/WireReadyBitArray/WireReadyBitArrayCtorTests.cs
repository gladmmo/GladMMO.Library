using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Reinterpret.Net;

namespace GladMMO.Tests.Collections
{
	[TestFixture]
	public sealed class WireReadyBitArrayCtorTests
	{
		[Test]
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(8)]
		[TestCase(8 * 2)]
		[TestCase(32)]
		[TestCase(32 * 2)]
		[TestCase(200)]
		[TestCase(65551)]
		public static void Ctor_Int(int length)
		{
			if(length % 8 != 0)
				return;

			WireReadyBitArray bitArray = new WireReadyBitArray(length);
			Assert.AreEqual(length, bitArray.Length);
			for(int i = 0; i < bitArray.InternalIntegerArray.Count; i++)
			{
				Assert.False(bitArray[i]);
				Assert.False(bitArray.Get(i));
			}
		}

		[Test]
		[TestCase(0, true)]
		[TestCase(0, false)]
		[TestCase(1, true)]
		[TestCase(1, false)]
		[TestCase(8, true)]
		[TestCase(8, false)]
		[TestCase(8 * 2, true)]
		[TestCase(8 * 2, false)]
		[TestCase(32, true)]
		[TestCase(32, false)]
		[TestCase(32 * 2, true)]
		[TestCase(32 * 2, false)]
		[TestCase(200, true)]
		[TestCase(200, false)]
		[TestCase(65551, true)]
		[TestCase(65551, false)]
		public static void Ctor_Int_Bool(int length, bool defaultValue)
		{
			//TODO: We now only accept things divisible by 8
			if(length % 8 != 0)
				return;

			WireReadyBitArray bitArray = new WireReadyBitArray(length, defaultValue);
			Assert.AreEqual(length, bitArray.Length);
			for(int i = 0; i < bitArray.Length; i++)
			{
				Assert.AreEqual(defaultValue, bitArray[i]);
				Assert.AreEqual(defaultValue, bitArray.Get(i));
			}
		}

		[Test]
		public static void Ctor_Int_NegativeLength_ThrowsArgumentOutOfRangeException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new WireReadyBitArray(-1));
			Assert.Throws<ArgumentOutOfRangeException>(() => new WireReadyBitArray(-1, false));
		}

		[Test]
		public static void Ctor_NullBoolArray_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new WireReadyBitArray((byte[])null));
		}

		public static IEnumerable<object[]> Ctor_BitArray_TestData()
		{
			yield return new object[] { "bool[](empty)", new WireReadyBitArray(new byte[0]) };

			foreach(int size in new[] { 32, 32 * 2, 32 * 4 })
			{
				yield return new object[] { "length", new WireReadyBitArray(size) };
				yield return new object[] { "length|default(true)", new WireReadyBitArray(size, true) };
				yield return new object[] { "length|default(false)", new WireReadyBitArray(size, false) };
				yield return new object[] { "bool[](all)", new WireReadyBitArray(Enumerable.Repeat(true, size).ToArray().Reinterpret().ReinterpretToArray<byte>()) };
				yield return new object[] { "bool[](none)", new WireReadyBitArray(Enumerable.Repeat(false, size).ToArray().Reinterpret().ReinterpretToArray<byte>()) };
				yield return new object[] { "bool[](alternating)", new WireReadyBitArray(Enumerable.Range(0, size).Select(x => x % 2 == 0).ToArray().Reinterpret().ReinterpretToArray<byte>()) };
				if(size >= 8)
				{
					yield return new object[] { "byte[](all)", new WireReadyBitArray(Enumerable.Repeat((byte)0xff, size / 8).ToArray().ReinterpretToArray<byte>()) };
					yield return new object[] { "byte[](none)", new WireReadyBitArray(Enumerable.Repeat((byte)0x00, size / 8).ToArray().ReinterpretToArray<byte>()) };
					yield return new object[] { "byte[](alternating)", new WireReadyBitArray(Enumerable.Repeat((byte)0xaa, size / 8).ToArray().ReinterpretToArray<byte>()) };
				}
				if(size >= 32)
				{
					yield return new object[] { "int[](all)", new WireReadyBitArray(Enumerable.Repeat(unchecked((byte)0xffffffff), size / 32).ToArray()) };
					yield return new object[] { "int[](none)", new WireReadyBitArray(Enumerable.Repeat((byte)0x00000000, size / 32).ToArray()) };
					yield return new object[] { "int[](alternating)", new WireReadyBitArray(Enumerable.Repeat(unchecked((byte)0xaaaaaaaa), size / 32).ToArray()) };
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(Ctor_BitArray_TestData))]
		public static void Ctor_BitArray(string label, WireReadyBitArray bits)
		{
			WireReadyBitArray bitArray = new WireReadyBitArray(bits);
			Assert.AreEqual(bits.Length, bitArray.Length, label);
			for(int i = 0; i < bitArray.Length; i++)
			{
				Assert.AreEqual(bits[i], bitArray[i]);
				Assert.AreEqual(bits[i], bitArray.Get(i));
			}
		}

		[Test]
		public static void Ctor_NullBitArray_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new WireReadyBitArray((WireReadyBitArray)null));
		}

		public static IEnumerable<object[]> Ctor_IntArray_TestData()
		{
			yield return new object[] { new byte[0], new bool[0] };
			foreach(int size in new[] { 1, 10 })
			{
				yield return new object[] { Enumerable.Repeat((byte)255, size).ToArray(), Enumerable.Repeat(true, size * 8).ToArray() };
				yield return new object[] { Enumerable.Repeat((byte)0x00000000, size).ToArray(), Enumerable.Repeat(false, size * 8).ToArray() };
				yield return new object[] { Enumerable.Repeat(unchecked((byte)0xaaaaaaaa), size).ToArray(), Enumerable.Range(0, size * 8).Select(i => i % 2 == 1).ToArray() };
			}
		}

		[Test]
		[TestCaseSource(nameof(Ctor_IntArray_TestData))]
		public static void Ctor_IntArray(byte[] array, bool[] expected)
		{
			WireReadyBitArray bitArray = new WireReadyBitArray(array);
			Assert.AreEqual(expected.Length, bitArray.Length);
			for(int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], bitArray[i], $"Index: {i} failed indexer.");
				Assert.AreEqual(expected[i], bitArray.Get(i), $"Index: {i} failed direct Get.");
			}
		}

		[Test]
		public static void Ctor_NullIntArray_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new WireReadyBitArray((byte[])null));
		}

		[Test]
		public static void Ctor_LargeIntArrayOverflowingBitArray_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => new WireReadyBitArray(new byte[int.MaxValue / 8 + 1]));
		}

		public static IEnumerable<object[]> Ctor_ByteArray_TestData()
		{
			yield return new object[] { new byte[0], new bool[0] };
			foreach(int size in new[] { 32, 32 * 2, 32 * 3, 32 * 4 })
			{
				yield return new object[] { Enumerable.Repeat((byte)0xff, size).ToArray(), Enumerable.Repeat(true, size * 8).ToArray() };
				yield return new object[] { Enumerable.Repeat((byte)0x00, size).ToArray(), Enumerable.Repeat(false, size * 8).ToArray() };
				yield return new object[] { Enumerable.Repeat((byte)0xaa, size).ToArray(), Enumerable.Range(0, size * 8).Select(i => i % 2 == 1).ToArray() };
			}
		}

		[Test]
		[TestCaseSource(nameof(Ctor_ByteArray_TestData))]
		public static void Ctor_ByteArray(byte[] bytes, bool[] expected)
		{
			WireReadyBitArray bitArray = new WireReadyBitArray(bytes.ReinterpretToArray<byte>());
			Assert.AreEqual(expected.Length, bitArray.Length);
			for(int i = 0; i < bitArray.Length; i++)
			{
				Assert.AreEqual(expected[i], bitArray[i]);
				Assert.AreEqual(expected[i], bitArray.Get(i));
			}
		}

		[Test]
		public static void Ctor_LargeByteArrayOverflowingBitArray_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => new WireReadyBitArray(new byte[int.MaxValue / 8 + 1]));
		}
	}
}