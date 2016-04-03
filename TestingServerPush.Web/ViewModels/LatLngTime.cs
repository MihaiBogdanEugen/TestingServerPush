using System;

namespace TestingServerPush.Web.ViewModels
{
    public class LatLngTime
    {
        public LatLngTime()
        {

        }

        public LatLngTime(LatLng point, DateTime addedAt)
        {
            this.Point = point;
            this.AddedAt = addedAt;
        }

        public LatLngTime(decimal lat, decimal lng, DateTime addedAt)
        {
            this.Point = new LatLng
            {
                Lat = lat,
                Lng = lng
            };
            this.AddedAt = addedAt;
        }

        public LatLng Point
        {
            get; set;
        }
        public DateTime AddedAt
        {
            get; set;
        }
    }
}