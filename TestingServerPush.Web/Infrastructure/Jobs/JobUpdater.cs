using System;
using System.Data.Entity;
using System.Linq;
using Hangfire;
using Microsoft.AspNet.SignalR;
using TestingServerPush.Web.Infrastructure.Hubs;
using TestingServerPush.Web.Models;
using TestingServerPush.Web.ViewModels;

namespace TestingServerPush.Web.Infrastructure.Jobs
{
    public class JobUpdater : IDisposable
    {
        private readonly IHubContext _hubContext;
        private readonly ServerPushDbContext _dbContext;

        public JobUpdater()
            : this(GlobalHost.ConnectionManager.GetHubContext<JobHub>(), new ServerPushDbContext())
        {
        }

        internal JobUpdater(IHubContext hubContext, ServerPushDbContext dbContext)
        {
            if (hubContext == null)
                throw new ArgumentNullException(nameof(hubContext));

            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this._hubContext = hubContext;
            this._dbContext = dbContext;
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }

        public void Update(int jobId)
        {
            var job = this._dbContext
                .Jobs
                .Include(x => x.Status)
                .Include(x => x.Actions)
                .Include(x => x.Positions)
                .FirstOrDefault(x => x.Id == jobId);

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

            this._hubContext.Clients.Group(JobHub.GetGroup(job.Id))
                .update(job.Id, job.Status.Name, job.Resolution, actions, positions);

            if (job.StatusId != 3)
                return;

            job.IsInProgress = false;
            this._dbContext.Entry(job).State = EntityState.Modified;
            this._dbContext.SaveChanges();

            RecurringJob.RemoveIfExists("update-job-#" + job.Id);
        }
    }
}
