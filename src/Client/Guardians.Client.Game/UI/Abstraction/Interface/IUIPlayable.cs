﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardians
{
	public interface IUIPlayable
	{
		bool isPlaying { get; }

		void Play();

		void Stop();
	}
}
