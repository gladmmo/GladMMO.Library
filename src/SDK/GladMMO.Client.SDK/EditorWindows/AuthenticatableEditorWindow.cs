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
	public abstract class AuthenticatableEditorWindow : EditorWindow
	{
		[SerializeField]
		private string _AccountName;

		[SerializeField]
		private string _Password;

		[HideInInspector]
		[SerializeField]
		private string _AuthToken;

		protected string AccountName => _AccountName;

		protected string Password => _Password;

		protected string AuthToken => _AuthToken;

		protected virtual void OnGUI()
		{
			//We just need to get auth details in
			_AccountName = EditorGUILayout.TextField("Account", AccountName);
			_Password = EditorGUILayout.PasswordField("Password", Password);
		}

		/// <summary>
		/// Attempts to authenticate with the set <see cref="AccountName"/>
		/// and <see cref="Password"/>
		/// </summary>
		/// <returns></returns>
		protected bool TryAuthenticate()
		{
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

			//TODO: Service discovery
			IAuthenticationService authService = Refit.RestService.For<IAuthenticationService>("https://auth.vrguardians.net");

			//Authentication using provided credentials
			JWTModel result = authService.TryAuthenticate(new AuthenticationRequestModel(AccountName, Password)).Result;

			Debug.Log($"Auth Result: {result.isTokenValid} Token: {result.AccessToken} Error: {result.Error} ErrorDescription: {result.ErrorDescription}.");

			_AuthToken = $"Bearer {result.AccessToken}";

			return result.isTokenValid;
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		protected bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
		}
	}
}
