using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GladMMO.Tests.Collections
{
	[TestFixture]
	public sealed class WireReadyBitArrayCustomTests
	{
		//This covers a fault where the enumeration of the wirereadybitarray
		//was throwing because it went outside the bounds of the array
		[Test]
		public void Test_WireReady_Index_Enumerable_Does_Not_Throw()
		{
			//arrange
			WireReadyBitArray bitArray = new WireReadyBitArray(16);

			//act
			bitArray.Set(3, true);

			//assert
			for(int i = 0; i < 16; i++)
			{
				Assert.DoesNotThrow(() => bitArray.Get(i));
			}

			Assert.AreEqual(16, bitArray.Length);
			Assert.DoesNotThrow(() => bitArray.EnumerateSetBitsByIndex().ToArray());
		}
	}
}