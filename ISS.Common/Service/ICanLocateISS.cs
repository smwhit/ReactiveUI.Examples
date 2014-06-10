using System;
using System.Threading.Tasks;
using ServiceStack;

namespace ISS.Common
{
	public interface ICanLocateISS
	{
		Task<ISSPassRoot> GetPasses(ISSLocationRequest request);
	}

}

