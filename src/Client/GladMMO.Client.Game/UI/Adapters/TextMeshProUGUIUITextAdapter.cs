using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using TMPro;

namespace GladMMO
{
	public sealed class TextMeshProUGUIUITextAdapter : MonoBehaviour, IUIText
	{
		[SerializeField]
		private TextMeshProUGUI TextObject;

		public string Text
		{
			get => TextObject.text;
			set => TextObject.text = value;
		}
	}
}
