using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using TestingServerPush.Updater.Properties;
using TestingServerPush.Web.Models;
using TestingServerPush.Web.ViewModels;

namespace TestingServerPush.Updater
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("START");

            Updater();

            Console.WriteLine("END");
            Console.ReadKey();
        }

        private static void Updater()
        {
            Console.WriteLine("Sleeping for 30 seconds...");
            Thread.Sleep(30000);
            Console.WriteLine("Sleeping finished");

            UpdateJob(Settings.Default.JobId, 2, "Job is in progress", "Worker started processing this job");

            var points = GetPointsInBetween(new LatLngTime
            {
                AddedAt = new DateTime(2016, 4, 3, 3, 14, 15),
                Point = new LatLng
                {
                    Lat = 44.4415775m,
                    Lng = 26.0179001m
                }
            }, new LatLngTime
            {
                AddedAt = new DateTime(2016, 4, 3, 3, 30, 15),
                Point = new LatLng { Lat = 44.483285m, Lng = 26.0979763m }
            });

            foreach (var point in points)
            {
                using (var dbContext = new ServerPushDbContext())
                {
                    var job = dbContext.Jobs.FirstOrDefault(x => x.Id == Settings.Default.JobId);
                    if (job == null)
                        return;

                    job.Positions.Add(new JobPosition
                    {
                        AddedAt = point.AddedAt,
                        Latitude = point.Point.Lat,
                        Longitude = point.Point.Lng
                    });

                    dbContext.Entry(job).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                Thread.Sleep(60000);
            }

            Console.WriteLine("Sleeping for 30 seconds...");
            Thread.Sleep(30000);
            Console.WriteLine("Sleeping finished");

            UpdateJob(Settings.Default.JobId, 3, "Job is finished", "Worker finished processing this job");
        }

        private static void UpdateJob(int jobId, int statusId, string resolution, string action)
        {
            using (var dbContext = new ServerPushDbContext())
            {
                var job = dbContext.Jobs.FirstOrDefault(x => x.Id == jobId);
                if (job == null)
                    return;

                job.StatusId = statusId;
                job.Resolution = resolution;

                job.Actions.Add(new JobAction
                {
                    AddedAt = DateTime.Now,
                    Name = action,
                });

                dbContext.Entry(job).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        private static IEnumerable<LatLngTime> GetPointsInBetween(LatLngTime start, LatLngTime end)
        {
            var result = new List<LatLngTime>();

            var totalTimeMinutes = (end.AddedAt - start.AddedAt).TotalMinutes;
            var segmentLength = (decimal)(1 / totalTimeMinutes);

            var startPoint = start;
            for (var index = 0; index < totalTimeMinutes; index++)
            {
                var addedAt = startPoint.AddedAt.AddMinutes(1);
                var newPoint = (start.Point - end.Point) * segmentLength + startPoint.Point;
                startPoint = new LatLngTime(newPoint, addedAt);
                result.Add(startPoint);
            }

            result.Insert(0, start);
            result.Add(end);

            return result;
        }
    }
}
