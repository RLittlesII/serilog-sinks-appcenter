using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginSample
{
    public class ServerService : IServerService
    {
        public async Task<List<string>> GetServers()
        {
           return new List<string>
            {
                "Server 1",
                "Server 2",
                "Server 3"
            };
        }
    }
}
