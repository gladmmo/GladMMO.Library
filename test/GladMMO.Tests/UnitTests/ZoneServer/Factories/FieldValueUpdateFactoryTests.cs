using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fasterflect;
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
			ChangeTrackingEntityFieldDataCollectionDecorator collection = new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection(8));
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
			ChangeTrackingEntityFieldDataCollectionDecorator collection = new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection(8));
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

		[Test]
		public void Test_ChangeTracker_With_Multiple_Value_Indicates_No_Changes_After_Clearing_FieldValueUpdate()
		{
			//arrange
			ChangeTrackingEntityFieldDataCollectionDecorator collection = new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection(8));
			FieldValueUpdateFactory updateFactory = new FieldValueUpdateFactory();

			//act
			collection.SetFieldValue<int>(1, 5);
			collection.SetFieldValue<int>(2, 4);
			collection.SetFieldValue<int>(3, 7);
			collection.ClearTrackedChanges();
			FieldValueUpdate fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));
			

			//assert
			Assert.AreEqual(0, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(0, fieldValueUpdate.FieldValueUpdates.Count, $"Field updates should be empty due to no changes..");
		}

		[Test]
		public void Test_ChangeTracker_With_Multiple_Value_Indicates_Correct_After_Clearing_Then_Setting_FieldValueUpdate()
		{
			//arrange
			ChangeTrackingEntityFieldDataCollectionDecorator collection = new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection(8));
			FieldValueUpdateFactory updateFactory = new FieldValueUpdateFactory();

			//act
			collection.SetFieldValue<int>(1, 5);
			collection.SetFieldValue<int>(2, 4);
			collection.SetFieldValue<int>(3, 7);
			collection.ClearTrackedChanges();
			FieldValueUpdate fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));

			collection.SetFieldValue<int>(2, 77);
			collection.SetFieldValue<int>(3, 89);
			fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));

			//assert
			Assert.AreEqual(2, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(2, fieldValueUpdate.FieldValueUpdates.Count, $"Field updates should be empty due to no changes..");
		}

		//Never remove this test. It's critical to prevent shared buffer fault that it covers.
		[Test]
		public void Test_ChangeTracker_Doesnt_Set_Change_Bits_On_Same_Value_After_Clear_FieldValueUpdate()
		{
			//arrange
			WireReadyBitArray bitArray = new WireReadyBitArray(1328);
			bitArray.Set(1, true);
			bitArray.Set(2, true);
			bitArray.Set(4, true);

			//Reference the actual client's visibile field update computation.
			IEntityDataFieldContainer dataCollection = NetworkVisibilityCreationBlockToVisibilityEventFactory.CreateInitialEntityFieldContainer(new FieldValueUpdate(bitArray, new int[] {5, 4, 7}));

			ChangeTrackingEntityFieldDataCollectionDecorator collection = new ChangeTrackingEntityFieldDataCollectionDecorator(dataCollection, bitArray);
			FieldValueUpdateFactory updateFactory = new FieldValueUpdateFactory();

			//act
			FieldValueUpdate fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));

			Assert.AreEqual(3, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(5, fieldValueUpdate.FieldValueUpdates.First(), $"Serialized value was not expected value.");
			Assert.AreEqual(1, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().First(), $"Index: {1} was expected to be first index.");

			collection.ClearTrackedChanges();

			
			//Check they're event before setting them again
			Assert.AreEqual(collection.GetFieldValue<int>(1), 5, $"Values not the same.");
			Assert.AreEqual(collection.GetFieldValue<int>(2), 4, $"Values not the same.");
			collection.SetFieldValue(1, 5);
			collection.SetFieldValue(2, 4);

			fieldValueUpdate = updateFactory.Create(new EntityFieldUpdateCreationContext(collection, collection.ChangeTrackingArray));

			//assert
			Assert.AreEqual(0, fieldValueUpdate.FieldValueUpdateMask.EnumerateSetBitsByIndex().Count(), $"Found more than 1 set bit.");
			Assert.AreEqual(0, fieldValueUpdate.FieldValueUpdates.Count, $"Field updates should be empty due to no changes..");
		}
	}
}
