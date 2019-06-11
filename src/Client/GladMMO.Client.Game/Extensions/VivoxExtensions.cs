using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public static class VivoxLoginSessionExtensions
	{
		/// <summary>
		/// The async version of Vivox BeginLogin.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="server"></param>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		public static Task LoginAsync([NotNull] this VivoxUnity.ILoginSession session, [NotNull] Uri server, [NotNull] string accessToken)
		{
			if (session == null) throw new ArgumentNullException(nameof(session));
			if (server == null) throw new ArgumentNullException(nameof(server));
			if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));

			return Task.Factory.FromAsync(session.BeginLogin(server, accessToken, null), session.EndLogin);
		}
	}
}
