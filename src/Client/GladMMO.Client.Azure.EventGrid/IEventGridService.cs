using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//Requires Head: aeg-sas-key: VXbGWce53249Mt8wuotr0GPmyJ/nDT4hgdEj9DpBeRr38arnnm5OFg== (not a real key)
	/// <summary>
	/// Contract for network service that can send
	/// </summary>
	public interface IEventGridService
	{
		//https://exampletopic.westus2-1.eventgrid.azure.net/api/events?api-version=2018-01-01
		[Post(@"/api/events?api-version=2020-08-01")]
		Task SendEventGridEventAsync<TModelType>([JsonBody] AzureEventGridRequest<TModelType> model)
			where TModelType : class;
	}
}
