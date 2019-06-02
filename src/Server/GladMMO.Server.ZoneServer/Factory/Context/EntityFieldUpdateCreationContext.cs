using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	/// <summary>
	/// Creation context for the 
	/// </summary>
	public sealed class EntityFieldUpdateCreationContext
	{
		public IEntityDataFieldContainer DataCollection { get; }

		public WireReadyBitArray FieldsToUpdateBitArray { get; }

		/// <inheritdoc />
		public EntityFieldUpdateCreationContext([NotNull] IEntityDataFieldContainer dataCollection, [NotNull] WireReadyBitArray fieldsToUpdateBitArray)
		{
			DataCollection = dataCollection ?? throw new ArgumentNullException(nameof(dataCollection));
			FieldsToUpdateBitArray = fieldsToUpdateBitArray ?? throw new ArgumentNullException(nameof(fieldsToUpdateBitArray));
		}
	}
}
