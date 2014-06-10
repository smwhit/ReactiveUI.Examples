using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISS.Common;

namespace ISS.Client.WPF
{
    public class FakeISSService : ICanLocateISS
    {
        public Task<ISSPassRoot> GetPasses(ISSLocationRequest request)
        {
            var root = new ISSPassRoot();
            root.Message = "ok";
            root.response = new List<ISSPass>
            {
                new ISSPass {Duration = 100, RiseTime = 140000012},
                new ISSPass {Duration = 74, RiseTime = 250000012},
                new ISSPass {Duration = 40, RiseTime = 2500000120},
            };

            return Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith((t, o) => root, null);
        }
    }
}