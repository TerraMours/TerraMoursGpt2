using Hangfire;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace TerraMours_Gpt.Domains.GptDomain.Hubs {

    [EnableCors("MyPolicy")]
    public class GraphGenerationHub : Hub {

        public GraphGenerationHub() {
        }

        public long GetWaitingCount() {
            return JobStorage.Current.GetMonitoringApi()
                .EnqueuedCount("img-queue");
        }
    }
}
