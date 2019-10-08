using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO.SDK;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IWorldTeleporterDataServiceClient : IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>
	{
		
	}
}
