using UnityEngine;
using Newtonsoft.Json;
using System; using FreecraftCore;

namespace GladMMO
{
	//TODO: Refactor to share serialization code.
	public class Vector4Converter : JsonConverter<Vector4>
	{
		public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//Ignore existing value.

			var t = serializer.Deserialize(reader);
			var iv = JsonConvert.DeserializeObject<Vector4>(t.ToString());
			return iv;
		}

		public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
		{
			Vector4 v = (Vector4)value;

			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(v.x);
			writer.WritePropertyName("y");
			writer.WriteValue(v.y);
			writer.WritePropertyName("z");
			writer.WriteValue(v.z);
			writer.WritePropertyName("w");
			writer.WriteValue(v.w);
			writer.WriteEndObject();
		}
	}

	public class Vector3ArrayConverter : JsonConverter<Vector3[]>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, Vector3[] value, JsonSerializer serializer)
		{
			Vector3Converter singleConverter = new Vector3Converter();

			writer.WriteStartArray();
			for(int i = 0; i < value.Length; i++)
			{
				ref Vector3 refVector = ref value[i];
				singleConverter.WriteJson(writer, refVector, serializer);
			}
			writer.WriteEndArray();
		}

		/// <inheritdoc />
		public override Vector3[] ReadJson(JsonReader reader, Type objectType, Vector3[] existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var t = serializer.Deserialize(reader);
			var iv = JsonConvert.DeserializeObject<Vector3[]>(t.ToString());
			return iv;
		}
	}

	public class Vector3Converter : JsonConverter<Vector3>
	{
		public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//Ignore existing value.

			var t = serializer.Deserialize(reader);
			var iv = JsonConvert.DeserializeObject<Vector3>(t.ToString());
			return iv;
		}

		public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
		{
			Vector3 v = (Vector3)value;

			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(v.x);
			writer.WritePropertyName("y");
			writer.WriteValue(v.y);
			writer.WritePropertyName("z");
			writer.WriteValue(v.z);
			writer.WriteEndObject();
		}

		public void WriteJson(JsonWriter writer, ref Vector3 value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(value.x);
			writer.WritePropertyName("y");
			writer.WriteValue(value.y);
			writer.WritePropertyName("z");
			writer.WriteValue(value.z);
			writer.WriteEndObject();
		}
	}

	public class Vector2Converter : JsonConverter<Vector2>
	{
		public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//Ignore existing value.

			var t = serializer.Deserialize(reader);
			var iv = JsonConvert.DeserializeObject<Vector2>(t.ToString());
			return iv;
		}

		public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
		{
			Vector2 v = (Vector2)value;

			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(v.x);
			writer.WritePropertyName("y");
			writer.WriteValue(v.y);
			writer.WriteEndObject();
		}
	}
}
