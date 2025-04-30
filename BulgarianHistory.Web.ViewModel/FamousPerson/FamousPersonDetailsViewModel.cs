using BulgarianHistory.Web.ViewModel.Common;
using System.Collections.Generic;

namespace BulgarianHistory.Web.ViewModel.FamousPerson
{
    public class FamousPersonDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string EraName { get; set; }

        public List<SimpleItemWithYearViewModel> Events { get; set; } = new();
    }

    public class SimpleItemWithYearViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
