using System;
using System.Collections.Generic;
using System.ComponentModel;
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

			return Task.Factory.FromAsync(session.BeginLogin(server, accessToken, new AsyncCallback(ar => {})), session.EndLogin);
		}
	}

	public static class VivoxChannelSessionExtensions
	{
		public static Task ConnectionAsync([NotNull] this VivoxUnity.IChannelSession channelSession, bool connectAudio, bool connectText, VivoxUnity.TransmitPolicy transmit, string accessToken)
		{
			if (channelSession == null) throw new ArgumentNullException(nameof(channelSession));
			if (!Enum.IsDefined(typeof(VivoxUnity.TransmitPolicy), transmit)) throw new InvalidEnumArgumentException(nameof(transmit), (int) transmit, typeof(VivoxUnity.TransmitPolicy));

			return Task.Factory.FromAsync(channelSession.BeginConnect(connectAudio, connectText, transmit, accessToken, new AsyncCallback(ar => { })), channelSession.EndConnect);
		}
	}
}
