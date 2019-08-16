using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginSample
{
    public interface IServerService
    {
        Task<List<string>> GetServers();
    }
}