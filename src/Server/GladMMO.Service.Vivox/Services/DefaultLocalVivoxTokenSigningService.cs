using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GladMMO
{
	public sealed class DefaultLocalVivoxTokenSigningService : IVivoxTokenSignService
	{
		//TODO: Handle this better by injecting and hiding where it comes from
		private static string VIVOX_API_KEY { get; }

		static DefaultLocalVivoxTokenSigningService()
		{
			//TODO: Make enviroment variable name a constant somewhere.
			VIVOX_API_KEY = Environment.GetEnvironmentVariable(SecurityEnvironmentVariables.VIVOX_API_KEY_PATH);
		}

		public string CreateSignature([JetBrains.Annotations.NotNull] VivoxTokenClaims claims)
		{
			if (claims == null) throw new ArgumentNullException(nameof(claims));

			string claimsString = JsonConvert.SerializeObject(claims);

			//Base64URLEncoding from: https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/master/src/Microsoft.IdentityModel.Tokens/Base64UrlEncoder.cs
			claimsString = Base64UrlEncoder.Encode(claimsString);

			//e30 is {} header
			string signable = $"e30.{claimsString}";

			return $"{signable}.{SHA256Hash(VIVOX_API_KEY, signable)}";
		}

		private static string SHA256Hash([JetBrains.Annotations.NotNull] string secret, [JetBrains.Annotations.NotNull] string message)
		{
			if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(secret));
			if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

			//TODO: Optimize
			byte[] keyByte = System.Text.Encoding.ASCII.GetBytes(secret);
			byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);

			using(var hmacsha256 = new HMACSHA256(keyByte))
			{
				byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
				return Base64UrlEncoder.Encode(hashmessage);
			}
		}
	}
}
