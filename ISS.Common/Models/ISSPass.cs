using System;
using System.Collections.Generic;

namespace ISS.Common
{
	public class ISSPass
	{
		public long RiseTime {get; set;}

		public long Duration { get; set; }
	}

	public class ISSPassRoot
	{
		public List<ISSPass> response { get; set; }

		public string Message { get; set; }
	}
}

