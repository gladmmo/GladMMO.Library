﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IAxisInputController
	{
		float CurrentHorizontal { get; }

		float CurrentVertical { get; }
	}
}
