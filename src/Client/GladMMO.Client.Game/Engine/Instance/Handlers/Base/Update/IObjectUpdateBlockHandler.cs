using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public interface IObjectUpdateBlockHandler
	{
		ObjectUpdateType UpdateType { get; }

		void HandleUpdateBlock(ObjectUpdateBlock updateBlock);
	}

	public interface IObjectUpdateBlockHandler<in TSpecificUpdateBlockType> : IObjectUpdateBlockHandler
		where TSpecificUpdateBlockType : ObjectUpdateBlock
	{
		void HandleUpdateBlock(TSpecificUpdateBlockType updateBlock);
	}
}