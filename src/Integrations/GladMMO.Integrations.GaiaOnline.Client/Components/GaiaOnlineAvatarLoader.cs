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
	public sealed class GaiaOnlineAvatarLoader : MonoBehaviour, IAvatarInitializable
	{
		//These should be the renderers to initialize the Avatar image to.
		[SerializeField]
		[Tooltip("List of renderers to set the loaded Avatar image to.")]
		private Renderer[] AvatarRenderers = new Renderer[0];

		public static Lazy<Shader> StandardShaderReference = new Lazy<Shader>(() => Shader.Find("Standard"));

		private void Start()
		{
			if (StandardShaderReference.Value == null)
				throw new InvalidOperationException($"The {nameof(StandardShaderReference)} cannot be null.");

			if(AvatarRenderers == null)
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be null.");

			if(!AvatarRenderers.Any())
				throw new InvalidOperationException($"The {nameof(AvatarRenderers)} cannot be empty.");
		}

		private async Task InitializeAvatarRenderers(string avatarName)
		{
			//TODO: Replace hardcoded name that is for testing.
			UserAvatarQueryResponse avatarQueryResponse = await GaiaOnlineIntegrationClientSingleton.QueryClient.GetAvatarFromUsernameAsync(avatarName)
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

			foreach (Renderer r in AvatarRenderers)
			{
				//For some reason we need to do this otherwise the actual
				//avatars are faded for some reason. I explored other options
				//but this seems the most viable option.
				r.material.shader = StandardShaderReference.Value;

				r.material.SetFloat("_Mode", 2);
				r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				r.material.SetInt("_ZWrite", 0);
				r.material.DisableKeyword("_ALPHATEST_ON");
				r.material.EnableKeyword("_ALPHABLEND_ON");
				r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				r.material.renderQueue = 3000;

				r.material.mainTexture = texture;
			}
		}

		public void InitializeAvatar([NotNull] NetworkEntityGuid entityGuidOwner, [NotNull] Task<string> entityName)
		{
			if (entityGuidOwner == null) throw new ArgumentNullException(nameof(entityGuidOwner));
			if (entityName == null) throw new ArgumentNullException(nameof(entityName));

			//We can't really recover from this but we can log
			try
			{
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					try
					{
						string avatarName = await entityName
							.ConfigureAwait(false);

						await InitializeAvatarRenderers(avatarName)
							.ConfigureAwait(false);
					}
					catch(Exception e)
					{
						Debug.LogError($"Failed to query GaiaOnline for avatar data. Reason: {e.ToString()}");
						throw;
					}
				});
			}
			catch(Exception e)
			{
				//TODO: Add username/id info
				Debug.LogError($"Encountered error when attempting to load the avatar: {e.Message}");
				throw;
			}
		}
	}
}
