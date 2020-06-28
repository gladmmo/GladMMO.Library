using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public interface IAuraApplicationCollection : IReadonlyAuraApplicationCollection, IEnumerable<ClientAuraApplicationData>
	{
		/// <summary>
		/// Applies the data at the specified index with <see cref="data"/>
		/// </summary>
		/// <param name="data">The data.</param>
		void Apply(ClientAuraApplicationData data);

		/// <summary>
		/// Removes the data at the specified index within <see cref="data"/>.
		/// </summary>
		/// <param name="data">The data.</param>
		void Remove(ClientAuraApplicationData data);

		/// <summary>
		/// Updates the data at the specified index within <see cref="data"/>
		/// </summary>
		/// <param name="data">The aura application update data.</param>
		void Update(ClientAuraApplicationData data);

		/// <summary>
		/// Removes the data at the specified index <see cref="slot"/>.
		/// </summary>
		/// <param name="slot">The aura slot to clear.</param>
		void Remove(byte slot);
	}

	public interface IReadonlyAuraApplicationCollection : IEnumerable<ClientAuraApplicationData>
	{
		/// <summary>
		/// Indicates if the aura slot is active or empty.
		/// </summary>
		/// <param name="auraSlot">The 8bit aura slot id.</param>
		/// <returns>True if the aura slot is active.</returns>
		bool IsSlotActive(byte auraSlot);

		/// <summary>
		/// Accesses the 
		/// </summary>
		/// <param name="slot">The aura slot.</param>
		/// <returns>The aura application data.</returns>
		ClientAuraApplicationData this[byte slot] { get; }
	}
}
