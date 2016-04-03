using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hangfire;
using TestingServerPush.Web.Infrastructure.Jobs;
using TestingServerPush.Web.Models;
using TestingServerPush.Web.Properties;
using TestingServerPush.Web.ViewModels;

namespace TestingServerPush.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServerPushDbContext _dbContext = new ServerPushDbContext();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var records = await this._dbContext
                .Jobs
                .Include(x => x.Status)
                .Include(x => x.Actions)
                .Select(x => new JobListViewModel
                {
                    Id = x.Id,
                    StatusName = x.Status.Name,
                    Name = x.Name,
                    AddedAt = x.AddedAt,
                    Resolution = x.Resolution,
                })
                .ToListAsync();

            return this.View(records);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id.HasValue == false)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var record = await this._dbContext
                .Jobs
                .Include(x => x.Status)
                .Include(x => x.Actions)
                .Select(x => new JobDetailsViewModel
                {
                    Id = x.Id,
                    StatusName = x.Status.Name,
                    Name = x.Name,
                    AddedAt = x.AddedAt,
                    Resolution = x.Resolution,
                    IsInProgress = x.IsInProgress,
                    Actions = x.Actions.Select(y => new ActionListViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        AddedAt = y.AddedAt,
                    })
                })
                .FirstOrDefaultAsync(x => x.Id == id.Value);

            this.ViewBag.GoogleMapsApiKey = Settings.Default.GoogleMapsApiKey;
            this.ViewBag.DefaultLocation = Settings.Default.DefaultLocation;

            return this.View(record);
        }

        [HttpGet]
        public ActionResult New()
        {
            return this.View(new JobNewViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> New(JobNewViewModel model)
        {
            if (this.ModelState.IsValid == false)
                return this.View(model);

            var newStatus = await this._dbContext.Statuses.FirstOrDefaultAsync(x => x.Name == "New");
            if (newStatus == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            var entity = new Job
            {
                AddedAt = DateTime.Now,
                Name = model.Name,
                StatusId = newStatus.Id,
            };
            entity.Actions.Add(new JobAction
            {
                AddedAt = DateTime.Now,
                Name = "New job added",
            });

            this._dbContext.Jobs.Add(entity);
            await this._dbContext.SaveChangesAsync();

            RecurringJob.AddOrUpdate<JobUpdater>("update-job-#" + entity.Id, x => x.Update(entity.Id), Cron.Minutely);

            return this.RedirectToAction("Details", new {id = entity.Id});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this._dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}