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
		private void OnGUI()
		{
			//Depending on authentication state
			//we dispatch the rendering for GUI controls
			if(AuthenticationModelSingleton.Instance.isAuthenticated)
				AuthenticatedOnGUI();
			else
				UnAuthenticatedOnGUI();
		}

		/// <summary>
		/// OnGUI called when the user is authenticated.
		/// </summary>
		protected abstract void AuthenticatedOnGUI();

		/// <summary>
		/// OnGUI called when the user is not authenticated.
		/// </summary>
		protected abstract void UnAuthenticatedOnGUI();
	}
}
