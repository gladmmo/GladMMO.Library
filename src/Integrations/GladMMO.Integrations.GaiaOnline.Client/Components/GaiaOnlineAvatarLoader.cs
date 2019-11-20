using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using Unitysync.Async;

namespace GladMMO.GaiaOnline
{
	public sealed class GaiaOnlineAvatarLoader : MonoBehaviour
	{
		//These should be the renderers to initialize the Avatar image to.
		[SerializeField]
		[Tooltip("List of renderers to set the loaded Avatar image to.")]
		private Renderer[] AvatarRenderers = new Renderer[0];

		[SerializeField]
		private bool InitializeOnStart;

		private void Start()
		{
			if(AvatarRenderers == null)
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be null.");

			if(!AvatarRenderers.Any())
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be empty.");

			if(InitializeOnStart)
				Initialize();
		}

		/// <inheritdoc />
		public void Initialize()
		{
			Reinitialize();
		}

		/// <inheritdoc />
		public void Reinitialize()
		{
			//It's possible reinitialize was called while we were initializing the renderers
			//So we should stop the routines
			StopAllCoroutines();

			//We can't really recover from this but we can log
			try
			{
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () => { await InitializeAvatarRenderers(); });
			}
			catch(Exception e)
			{
				//TODO: Add username/id info
				Debug.LogError($"Encountered error when attempting to load the avatar: {e.Message}");
				throw;
			}
		}

		private async Task InitializeAvatarRenderers()
		{
			//TODO: Replace hardcoded name that is for testing.
			UserAvatarQueryResponse avatarQueryResponse = await GaiaOnlineIntegrationClientSingleton.QueryClient.GetAvatarFromUsernameAsync("HelloKittyGithub")
				.ConfigureAwait(false);

			//TODO: Somehow get ref to avatar name for logging.
			if (!avatarQueryResponse.isSuccessful)
				throw new InvalidOperationException($"Encounted Error Code: {avatarQueryResponse.ResponseStatusCode} when trying to request Avatar.");

			//Change the URL to get the strip
			string stripUrl = $"{avatarQueryResponse.AvatarRelativeUrlPath.Split('?').First().TrimEnd(".png".ToCharArray())}_strip.png";

			Texture2DWrapper imageAsync = await GaiaOnlineIntegrationClientSingleton.ImageCDNClient.GetAvatarImageAsync(stripUrl)
				.ConfigureAwait(false);

			//Just onto the main thread and check gameobject state before proceeding to initialize renderers
			//unity overloads == on some Types in UnityEngine namespace.
			await new UnityYieldAwaitable();

			if(this != null && gameObject != null)
				SetAvatarForRenderers(imageAsync.Texture.Value);
		}

		private void SetAvatarForRenderers(Texture2D texture)
		{
			if(texture == null) throw new ArgumentNullException(nameof(texture));

			foreach(Renderer r in AvatarRenderers)
				r.material.mainTexture = texture;
		}
	}
}
