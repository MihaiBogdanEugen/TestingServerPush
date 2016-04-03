using System.Collections.Generic;
using System.Linq;

namespace TestingServerPush.Web.ViewModels
{
    public class JobDetailsViewModel : JobListViewModel
    {
        public JobDetailsViewModel()
        {
            this.Actions = new List<ActionListViewModel>();
            this.Positions = new List<LatLngTime>();

            this.PickupLocationLatitude = 44.4415775m;
            this.PickupLocationLongitude = 26.0179001m;

            this.DeliveryLocationLatitude = 44.483285m;
            this.DeliveryLocationLongitude = 26.0979763m;
        }

        public bool IsInProgress { get; set; }

        public IEnumerable<ActionListViewModel> Actions { get; set; } 

        public IEnumerable<LatLngTime> Positions { get; set; }

        public LatLng[] PositionsAsPointsArray => this.Positions.OrderBy(x => x.AddedAt).Select(x => x.Point).ToArray();

        public decimal PickupLocationLatitude { get; set; }
        public decimal PickupLocationLongitude { get; set; }

        public decimal DeliveryLocationLatitude { get; set; }
        public decimal DeliveryLocationLongitude { get; set; }
    }
}