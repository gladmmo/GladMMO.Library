using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using ProtoBuf;

namespace GladMMO
{
	[TestFixture]
	public sealed class ProtobufModelTests : GenericModelTests<ProtoMemberAttribute, ProtoIgnoreAttribute, ProtoContractAttribute>
	{
		public static IEnumerable<Type> AllProtobufPayloadModels => ZoneServerMetadataMarker.PayloadTypes;

		public static IEnumerable<Type> AllProtobufModels => ZoneServerMetadataMarker.AllProtobufModels;

		[Test]
		public void Test_No_Duplicate_OpCodes_For_Same_PayloadType()
		{
			//arrange
			IEnumerable<Type> distinctPayloads = ModelTypes
				.Where(t => t.GetCustomAttribute<GamePayloadAttribute>(true) != null)
				.Distinct(new PayloadTypeEqualityComparerByOpCodeAndType())
				.ToArray();

			//assert
			Assert.AreEqual(ModelTypes.Count(t => t.GetCustomAttribute<GamePayloadAttribute>(true) != null), distinctPayloads.Count(), $"Encountered duplicate payload type More than 1 payload shares same OpCode and BaseType. Violating Types: {ModelTypes.Where(t => t.GetCustomAttribute<GamePayloadAttribute>(true) != null).Except(distinctPayloads).Aggregate("", (s, type) => $"{s} {type.Name}:OpCode.{type.GetCustomAttribute<GamePayloadAttribute>(true).OperationCode}")}");
		}

		[Test]
		[TestCaseSource(nameof(AllProtobufPayloadModels))]
		public void Test_All_Payloads_Have_GamePayloadAttribute(Type payloadType)
		{
			//assert
			Assert.True(payloadType.GetCustomAttribute<GamePayloadAttribute>() != null, $"Payload is missing {nameof(GamePayloadAttribute)}. Type: {payloadType.Name}");
		}

		[Test]
		[TestCaseSource(nameof(AllProtobufModels))]
		public void Test_All_ProtobufModels_Dont_Have_Duplicate_Keys(Type payloadType)
		{
			Console.WriteLine(payloadType.Name);
			//arrange
			ProtoMemberAttribute[] attributes = payloadType
				.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(info => info.GetCustomAttribute<ProtoMemberAttribute>() != null)
				.Select(info => info.GetCustomAttribute<ProtoMemberAttribute>())
				.ToArray();

			//assert
			Assert.AreEqual(attributes.Length, attributes.Distinct(new ProtoMemberKeyIdComparer()).Count(), $"Protobuf Model had duplicate keys. Type: {payloadType.Name}");
		}

		private class ProtoMemberKeyIdComparer : EqualityComparer<ProtoMemberAttribute>
		{
			/// <inheritdoc />
			public override bool Equals(ProtoMemberAttribute x, ProtoMemberAttribute y)
			{
				if(x == null)
					return y == null;
				else if(y == null)
					return false;

				return x.Tag == y.Tag;
			}

			/// <inheritdoc />
			public override int GetHashCode(ProtoMemberAttribute obj)
			{
				return obj.Tag;
			}
		}

		private class PayloadTypeEqualityComparerByOpCodeAndType : EqualityComparer<Type>
		{
			/// <inheritdoc />
			public override bool Equals(Type x, Type y)
			{
				return x.BaseType == y.BaseType && x.GetCustomAttribute<GamePayloadAttribute>(true).OperationCode == y.GetCustomAttribute<GamePayloadAttribute>(true).OperationCode;
			}

			/// <inheritdoc />
			public override int GetHashCode(Type obj)
			{
				return $"{obj.BaseType.Name}{obj.GetCustomAttribute<GamePayloadAttribute>(true).OperationCode}".GetHashCode();
			}
		}
	}
}
