using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultAddressableContentLoader : IAddressableContentLoader
	{
		public async Task<T> LoadContentAsync<T>(string address)
		{
			//TODO: Implement generalized loading.
			if (typeof(T) == typeof(Texture2D))
				return await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>("Interface/Icons/Trade_Engineering").Task;

			throw new NotImplementedException($"TODO: Implement: {typeof(T).Name} loading.");
		}
	}
}
