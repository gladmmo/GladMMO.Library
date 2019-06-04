using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public static class WireReadyBitArrayGetSetTests
	{
		private const int BitsPerByte = 8;
		private const int BitsPerInt32 = 32;

		public static IEnumerable<object[]> Get_Set_Data()
		{
			foreach(int size in new[] {0, BitsPerByte, BitsPerByte * 2, BitsPerInt32, BitsPerInt32 * 2})
			{
				foreach(bool def in new[] {true, false})
				{
					yield return new object[] {def, Enumerable.Repeat(true, size).ToArray()};
					yield return new object[] {def, Enumerable.Repeat(false, size).ToArray()};
					yield return new object[] {def, Enumerable.Range(0, size).Select(i => i % 2 == 1).ToArray()};
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(Get_Set_Data))]
		public static void Get_Set(bool def, bool[] newValues)
		{
			WireReadyBitArray WireReadyBitArray = new WireReadyBitArray(newValues.Length, def);
			for(int i = 0; i < newValues.Length; i++)
			{
				WireReadyBitArray.Set(i, newValues[i]);
				Assert.AreEqual(newValues[i], WireReadyBitArray[i]);
				Assert.AreEqual(newValues[i], WireReadyBitArray.Get(i));
			}
		}

		[Test]
		public static void Get_InvalidIndex_ThrowsArgumentOutOfRangeException()
		{
			WireReadyBitArray bitArray = new WireReadyBitArray(16);
			Assert.Throws<ArgumentOutOfRangeException>(() => bitArray.Get(-1));
			Assert.Throws<ArgumentOutOfRangeException>(() => bitArray.Get(bitArray.Length));

			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var l = bitArray[-1];
			});
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var l = bitArray[bitArray.Length];
			});
		}

		[Test]
		public static void Set_InvalidIndex_ThrowsArgumentOutOfRangeException()
		{
			WireReadyBitArray WireReadyBitArray = new WireReadyBitArray(16);
			Assert.Throws<ArgumentOutOfRangeException>(() => WireReadyBitArray.Set(-1, true));
			Assert.Throws<ArgumentOutOfRangeException>(() => WireReadyBitArray.Set(WireReadyBitArray.Length, true));

			Assert.Throws<ArgumentOutOfRangeException>(() => WireReadyBitArray[-1] = true);
			Assert.Throws<ArgumentOutOfRangeException>(() => WireReadyBitArray[WireReadyBitArray.Length] = true);
		}

		[Test]
		[TestCase(0, true)]
		[TestCase(0, false)]
		[TestCase(BitsPerByte, true)]
		[TestCase(BitsPerByte, false)]
		[TestCase(BitsPerInt32, true)]
		[TestCase(BitsPerInt32, false)]
		public static void SetAll(int size, bool defaultValue)
		{
			WireReadyBitArray WireReadyBitArray = new WireReadyBitArray(size, defaultValue);
			WireReadyBitArray.SetAll(!defaultValue);
			for(int i = 0; i < WireReadyBitArray.Length; i++)
			{
				Assert.AreEqual(!defaultValue, WireReadyBitArray[i]);
				Assert.AreEqual(!defaultValue, WireReadyBitArray.Get(i));
			}

			WireReadyBitArray.SetAll(defaultValue);
			for(int i = 0; i < WireReadyBitArray.Length; i++)
			{
				Assert.AreEqual(defaultValue, WireReadyBitArray[i]);
				Assert.AreEqual(defaultValue, WireReadyBitArray.Get(i));
			}
		}
	}
}
