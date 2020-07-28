using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Object = System.Object;

namespace GladMMO
{
	public static class GladMMOPrefabUtility
	{
		private static Func<UnityEngine.Object, UnityEngine.Object> InstantiatePrefabDelegate;

		//Reason: System.TypeInitializationException: The type initializer for 'GladMMO.GladMMOPrefabUtility' threw an exception. ---> System.TypeLoadException: Could not load type 'PrefabUtility' from assembly 'UnityEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.
		static GladMMOPrefabUtility()
		{
			Assembly editorAssembly = AppDomain
				.CurrentDomain
				.GetAssemblies()
				.First(a => a.GetName().Name.Contains("UnityEditor") && a.GetTypes().Any(t => t.Name.Contains("PrefabUtility")));

			MethodInfo prefabUtilityCreateMethodInfo = editorAssembly
				.GetTypes()
				.First(t => t.Name.Contains("PrefabUtility"))
				.GetMethod("InstantiatePrefab", BindingFlags.Public | BindingFlags.Static, null, CallingConventions.Any, new Type[1] { typeof(UnityEngine.Object) }, null);

			InstantiatePrefabDelegate = (Func<UnityEngine.Object, UnityEngine.Object>) Delegate.CreateDelegate(typeof(Func<UnityEngine.Object, UnityEngine.Object>), prefabUtilityCreateMethodInfo);
		}

		//See: https://docs.unity3d.com/ScriptReference/PrefabUtility.InstantiatePrefab.html
		public static UnityEngine.Object InstantiatePrefab(UnityEngine.Object assetComponentOrGameObject)
		{
			if (!Application.isEditor)
				throw new InvalidOperationException($"{nameof(InstantiatePrefab)} is only available in the Editor.");

			//Hacky reflection reference.
			return InstantiatePrefabDelegate(assetComponentOrGameObject);
		}
	}
}
