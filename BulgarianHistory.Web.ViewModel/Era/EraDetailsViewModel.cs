using BulgarianHistory.Web.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianHistory.Web.ViewModel.Details
{
    public class EraDetailsViewModel
    {
             public int Id { get; set; }
             public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public string ImageUrl { get; set; } = null!;

            public List<SimpleItemViewModel> Events { get; set; } = new();
            public List<SimpleItemViewModel> Cities { get; set; } = new();
            public List<SimpleItemViewModel> FamousPeople { get; set; } = new();
        }
    }

