using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface ICreatureDataServiceClient
	{
		[Get("/api/CreatureData/template/{id}")]
		Task<ResponseModel<CreatureTemplateModel, SceneContentQueryResponseCode>> GetCreatureTemplate([AliasAs("id")] int creatureTemplateId);
	}
}