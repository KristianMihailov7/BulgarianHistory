using System.Collections.Generic;
using BulgarianHistory.Web.ViewModel.Common;

namespace BulgarianHistory.Web.ViewModel.City
{
    public class CityDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public List<SimpleItemViewModel> RelatedEvents { get; set; }
        public List<SimpleItemViewModel> FamousPeople { get; set; }  // NEW
    }
}
