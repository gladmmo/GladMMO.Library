﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	public sealed class EnityDataChangedArgs<TValueType> : EventArgs
	{
		//TODO: We don't currently have a way to handle original value. It's not persisted.
		public TValueType OriginalValue { get; }

		public TValueType NewValue { get; }

		/// <inheritdoc />
		public EnityDataChangedArgs(TValueType originalValue, TValueType newValue)
		{
			OriginalValue = originalValue;
			NewValue = newValue;
		}
	}
}
