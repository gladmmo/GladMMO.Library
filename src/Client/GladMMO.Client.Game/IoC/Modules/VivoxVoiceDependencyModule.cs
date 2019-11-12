using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using UnityEngine;

namespace GladMMO
{
	public sealed class VivoxVoiceDependencyModule : Module
	{
		public static bool isInternalAndroidModuleInitialized { get; set; } = false;

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<VivoxAuthServiceDependencyAutofacModule>();

			//Only call this once.
			if (!isInternalAndroidModuleInitialized && Application.isMobilePlatform)
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject appContext = activity.Call<AndroidJavaObject>("getApplicationContext");
				AndroidJavaClass pluginClass = new AndroidJavaClass("com.vivox.vivoxnative.VivoxNative");
				pluginClass.CallStatic("init", appContext);

				isInternalAndroidModuleInitialized = true;
			}

			builder.RegisterType<VivoxUnity.Client>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<PositionalVoiceEntityTickable>()
				.AsSelf();

			builder.RegisterInstance(new DefaultPositionVoiceChannelsCollection())
				.AsImplementedInterfaces()
				.AsSelf()
				.SingleInstance();
		}
	}
}
