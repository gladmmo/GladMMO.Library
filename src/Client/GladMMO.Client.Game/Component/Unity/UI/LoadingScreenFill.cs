using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//TODO: This is kind of hacky, but only way to deal with cross-scene loading screens and tickables.
	public sealed class LoadingScreenFill : MonoBehaviour, IGameTickable
	{
		[SerializeField]
		private UnityEngine.UI.Image LoadingScreenFillImage;

		void Update()
		{
			Tick();
		}

		public void Tick()
		{
			if(!LoadingScreenOperationContainer.OperationStack.Any())
				return;

			LoadingScreenFillImage.fillAmount = LoadingScreenOperationContainer.OperationStack.Peek().PercentComplete;

			if(LoadingScreenOperationContainer.OperationStack.Peek().IsDone)
			{
				LoadingScreenFillImage.fillAmount = 1.0f;
				LoadingScreenOperationContainer.OperationStack.Pop();
			}
		}
	}
}
