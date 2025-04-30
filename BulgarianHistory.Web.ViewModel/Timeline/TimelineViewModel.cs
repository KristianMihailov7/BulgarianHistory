using BulgarianHistory.Data.Entities;
using System.Collections.Generic;

namespace BulgarianHistory.Web.ViewModel.Timeline
{
    public class TimelineViewModel
    {
        public List<Era> Eras { get; set; } = new();
        public List<BulgarianHistory.Data.Entities.Event> Events { get; set; } = new();
    }
}