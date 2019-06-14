using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class PlayerSpawnStrategyQueue : ConcurrentQueue<ISpawnPointStrategy>
	{

	}
}
