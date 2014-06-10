using System;
using System.Threading.Tasks;
using ServiceStack;
using Newtonsoft.Json;

namespace ISS.Common
{

	public class ISSLocationService : ICanLocateISS
	{
		public async Task<ISSPassRoot> GetPasses (ISSLocationRequest request)
		{
			var client = new System.Net.Http.HttpClient();
			var response = await client.GetAsync("http://api.open-notify.org/iss-pass.json?lat=51.404845&lon=-0.249210&n=50").ConfigureAwait(false);
			response.EnsureSuccessStatusCode ();

			var jsonPayload = await response.Content.ReadAsStringAsync ().ConfigureAwait(false);

			//var x = jsonPayload.FromJson<ISSPassRoot>();

			var x = JsonConvert.DeserializeObject<ISSPassRoot> (jsonPayload);

			return x;
			//return new ISSPassRoot () { Message = "ffs" };
		}
	}
}
