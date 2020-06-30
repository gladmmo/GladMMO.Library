using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public static class AuraBuffTypeExtensions
	{
		public static AuraBuffType ToBuffType(this AuraFlags flags)
		{
			return flags.HasAnyFlags(AuraFlags.NEGATIVE) ? AuraBuffType.Negative : AuraBuffType.Positive;
		}
	}

	//TODO: Shouldwe use a different enum?? Should we put this somewhere else?
	public enum AuraBuffType
	{
		Positive,
		Negative
	}

	public interface IUIAuraBuffCollection
	{
		IUIAuraBuffSlot this[AuraBuffType type, byte index] { get; }

		event EventHandler<AuraBuffClickedEventArgs> OnAuraBuffClicked;

		IEnumerable<IUIAuraBuffSlot> EnumerateActive();
	}

	public sealed class AuraBuffClickedEventArgs : EventArgs
	{
		public byte Slot { get; }

		public AuraBuffClickedEventArgs(byte slot)
		{
			Slot = slot;
		}
	}
}
