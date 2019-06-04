using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public sealed class FieldValueUpdateFactoryTests
	{
		[Test]
		public void Test_Can_Create_Factory_Without_Throwing()
		{
			Assert.DoesNotThrow(() => new FieldValueUpdateFactory());
		}

		[Test]
		public void Test_FieldValueFactory_With_Single_Value_Produces_Correct_FieldValueUpdate([EntityDataCollectionTestRange] int index, [Values(1, 2, 3, 4, 5, 6, 7, 8)] int value)
		{
			//arrange
			ChangeTrackingEntityFieldDataCollectionDecorator<TestFieldType> collection = new ChangeTrackingEntityFieldDataCollectionDecorator<TestFieldType>(new EntityFieldDataCollection<TestFieldType>());
			FieldValueUpdateFactory updateFactory = new FieldValueUpdateFactory();

			//act
			collection.SetFieldValue<int>(index, value);
			FieldValueUpdate fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));


			//assert
			Assert.AreEqual(1, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(value, fieldValueUpdate.FieldValueUpdates.First(), $"Serialized value was not expected value.");
			Assert.AreEqual(index, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().First(), $"Index: {index} was expected to be in the update but was not.");
		}

		[Test]
		public void Test_FieldValueFactory_With_Multiple_Value_Produces_Correct_FieldValueUpdate()
		{
			//arrange
			ChangeTrackingEntityFieldDataCollectionDecorator<TestFieldType> collection = new ChangeTrackingEntityFieldDataCollectionDecorator<TestFieldType>(new EntityFieldDataCollection<TestFieldType>());
			FieldValueUpdateFactory updateFactory = new FieldValueUpdateFactory();

			//act
			collection.SetFieldValue<int>(1, 5);
			collection.SetFieldValue<int>(2, 4);
			collection.SetFieldValue<int>(3, 7);
			FieldValueUpdate fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));


			//assert
			Assert.AreEqual(3, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(5, fieldValueUpdate.FieldValueUpdates.First(), $"Serialized value was not expected value.");
			Assert.AreEqual(1, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().First(), $"Index: {1} was expected to be first index.");
		}
	}
}
