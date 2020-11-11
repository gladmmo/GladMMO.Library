using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	[Obsolete("This hash Algorithm is no longer used by modern TrinityCore so we should not use it. SHA was never secure anyway.")]
	public sealed class TrinityCoreSHAPasswordHashAlgorithm : IPasswordHashingAlgorithm
	{
		//See: https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
		private static readonly uint[] _lookup32Unsafe = CreateLookup32Unsafe();
		private static readonly unsafe uint* _lookup32UnsafeP = (uint*)GCHandle.Alloc(_lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

		public string CreateHash(string username, string password)
		{
			if(username == null) throw new ArgumentNullException(nameof(username));
			if(password == null) throw new ArgumentNullException(nameof(password));


			//Insecure, but it's what World of Warcraft uses.
			using(SHA1 sha = new SHA1CryptoServiceProvider())
			{
				string hashInput = $"{username.ToUpper()}:{password.ToUpper()}";
				byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(hashInput));

				return ByteArrayToHexViaLookup32Unsafe(hash);
			}
		}

		private static uint[] CreateLookup32Unsafe()
		{
			var result = new uint[256];
			for(int i = 0; i < 256; i++)
			{
				string s = i.ToString("X2");
				if(BitConverter.IsLittleEndian)
					result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
				else
					result[i] = ((uint)s[1]) + ((uint)s[0] << 16);
			}

			return result;
		}

		public static unsafe string ByteArrayToHexViaLookup32Unsafe(byte[] bytes)
		{
			var lookupP = _lookup32UnsafeP;
			var result = new char[bytes.Length * 2];
			fixed(byte* bytesP = bytes)
			fixed(char* resultP = result)
			{
				uint* resultP2 = (uint*)resultP;
				for(int i = 0; i < bytes.Length; i++)
				{
					resultP2[i] = lookupP[bytesP[i]];
				}
			}

			return new string(result);
		}
	}
}
