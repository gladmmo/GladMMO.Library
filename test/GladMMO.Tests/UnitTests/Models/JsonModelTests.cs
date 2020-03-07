using System; using FreecraftCore;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public sealed class JsonModelTests : GenericModelTests<JsonPropertyAttribute, JsonIgnoreAttribute, JsonObjectAttribute>
	{
		[Test]
		[TestCaseSource(nameof(SerializableMembers))]
		public void Test_Model_All_Properties_Have_Setters_For_Unity3D_Compatibility(MemberInfo m)
		{
			//Only check props
			if(m.MemberType != MemberTypes.Property)
				return;

			//assert
			Assert.True(((PropertyInfo)m).CanWrite, $"{GenerateMemberInfoIdentiferPrefix(m)} is a property does not have a setter. All properties, due to Unity3D JSON compatibility, are required to have setters.");
		}
	}
}
