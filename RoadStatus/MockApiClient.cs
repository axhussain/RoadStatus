using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
    public class MockApiClient : IApiClient
    {
        public Task<List<RoadCorridor>> GetRoadStatus(string roadId)
        {
            var results = new List<RoadCorridor>
            {
                new RoadCorridor() { DisplayName = "A2", Id = "a2", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" }
            };

            //TaskCompletionSource represents the producer side of a Task<TResult> unbound to a delegate, 
            //providing access to the consumer side through the Task property.
            var tcs = new TaskCompletionSource<List<RoadCorridor>>();
            tcs.SetResult(results);
            return tcs.Task;
        }
    }
}
