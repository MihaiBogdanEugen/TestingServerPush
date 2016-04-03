using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using TestingServerPush.Web.Models;
using TestingServerPush.Web.ViewModels;

namespace TestingServerPush.Web.Infrastructure.Hubs
{
    public class JobHub : Hub
    {
        public async Task Subscribe(int jobId)
        {
            await this.Groups.Add(this.Context.ConnectionId, GetGroup(jobId));

            using (var dbContext = new ServerPushDbContext())
            {
                var job = await dbContext
                    .Jobs
                    .Include(x => x.Status)
                    .Include(x => x.Actions)
                    .Include(x => x.Positions)
                    .FirstOrDefaultAsync(x => x.Id == jobId);

                if (job == null)
                    return;

                if (job.StatusId == 1 || string.IsNullOrEmpty(job.Resolution) || job.Actions.Count == 1)
                    return;

                var actions = job.Actions.OrderByDescending(x => x.AddedAt).Select(x => new ActionListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    AddedAt = x.AddedAt,
                }).ToArray().AsJson();

                var positions = job.Positions.OrderBy(x => x.AddedAt).Select(x => new LatLng
                {
                    Lat = x.Latitude,
                    Lng = x.Longitude
                }).ToArray().AsJson();

                this.Clients.Client(this.Context.ConnectionId).update(job.Id, job.Status.Name, job.Resolution, actions, positions);
            }
        }

        public static string GetGroup(int jobId)
        {
            return "job:" + jobId;
        }
    }
}
