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
			{
				//TODO: Remove this hack for loading an icon.
				//Interface\Icons\Spell_Nature_FarSight
				//Assets/Content/Interface/Icons/Ability_BackStab.png
				return await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>($"Assets/Content/{address.Replace('\\','/')}.png").Task;
			}
			

			throw new NotImplementedException($"TODO: Implement: {typeof(T).Name} loading.");
		}
	}
}
