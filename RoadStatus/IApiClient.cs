using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
    public interface IApiClient
    {
        Task<List<RoadCorridor>> GetRoadStatus(string roadId);
    }
}
