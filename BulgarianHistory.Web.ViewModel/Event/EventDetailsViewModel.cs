using BulgarianHistory.Web.ViewModel.Common;
using BulgarianHistory.Web.ViewModel.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianHistory.Web.ViewModel.Event
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public string EraName { get; set; }

        public List<SimpleItemViewModel> Cities { get; set; }
        public List<FamousPersonInEventViewModel> FamousPeople { get; set; }
        public List<string> Facts { get; set; }
        public List<SourceViewModel> Sources { get; set; }
        public List<SimpleItemViewModel> RelatedEvents { get; set; }
        public List<FamousPersonInEventViewModel> FamousPeopleDetails { get; set; } = new();
    }

    public class SourceViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
    public class FamousPersonInEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
    }
}
