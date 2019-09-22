using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class SdkAuthenticationEditorWindow : AuthenticatableEditorWindow
	{
		[SerializeField]
		private string _AccountName;

		[SerializeField]
		private string _Password;

		[MenuItem("VRGuardians/Login")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(SdkAuthenticationEditorWindow));
		}

		protected override void AuthenticatedOnGUI()
		{
			EditorGUILayout.LabelField($"You are authenticated.");

			if (GUILayout.Button("Logout"))
				ClearAuthenticationData();
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//We just need to get auth details in
			_AccountName = EditorGUILayout.TextField("Account", _AccountName);
			_Password = EditorGUILayout.PasswordField("Password", _Password);

			if (GUILayout.Button("Login"))
			{
				if(!TryAuthenticate())
				{
					Debug.LogError($"Failed to authenticate User: {_AccountName}");
					return;
				}
			}
		}

		private void ClearAuthenticationData()
		{
			AuthenticationModelSingleton.Instance.SetAuthenticationState(false);
		}

		/// <summary>
		/// Attempts to authenticate with the set <see cref="AccountName"/>
		/// and <see cref="Password"/>
		/// </summary>
		/// <returns></returns>
		private bool TryAuthenticate()
		{
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.CheckCertificateRevocationList = false;

			//TODO: Service discovery
			IAuthenticationService authService = Refit.RestService.For<IAuthenticationService>("https://auth.vrguardians.net");

			//Authentication using provided credentials
			JWTModel result = authService.TryAuthenticate(new AuthenticationRequestModel(_AccountName, _Password)).Result;

			Debug.Log($"Auth Result: {result.isTokenValid} Token: {result.AccessToken} Error: {result.Error} ErrorDescription: {result.ErrorDescription}.");

			if (result.isTokenValid)
			{
				AuthenticationModelSingleton.Instance.SetAuthenticationState(true);
				AuthenticationModelSingleton.Instance.SetAuthenticationToken(result.AccessToken);
			}

			return result.isTokenValid;
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		private bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
		}
	}
}
