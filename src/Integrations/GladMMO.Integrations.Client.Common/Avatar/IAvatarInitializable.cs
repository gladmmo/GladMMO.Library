using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IAvatarInitializable
	{
		/// <summary>
		/// Initializes the avatar.
		/// </summary>
		/// <param name="entityGuidOwner">The entity guid of the avatar owner.</param>
		/// <param name="entityName">The awaitable name of the entity. (Task because it may not be available immediately upon initialization)</param>
		void InitializeAvatar(ObjectGuid entityGuidOwner, Task<string> entityName);
	}
}
