namespace TestingServerPush.Web.ViewModels
{
    public class LatLng
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }

        public static LatLng operator -(LatLng a, LatLng b)
        {
            return new LatLng { Lat = b.Lat - a.Lat, Lng = b.Lng - a.Lng};
        }

        public static LatLng operator +(LatLng a, LatLng b)
        {
            return new LatLng { Lat = a.Lat + b.Lat, Lng = a.Lng + b.Lng};
        }

        public static LatLng operator *(LatLng a, decimal d)
        {
            return new LatLng { Lat = a.Lat * d, Lng = a.Lng * d };
        }

        public override string ToString()
        {
            return $"[{(object) this.Lat:###.###############}, {(object) this.Lng:###.###############}]";
        }
    }
}