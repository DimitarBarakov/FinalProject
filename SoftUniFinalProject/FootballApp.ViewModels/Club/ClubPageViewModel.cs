using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.ViewModels.Club
{
    public class ClubPageViewModel
    {
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Nickname { get; set; }

        public string Stadium { get; set; } = null!;

        public List<ClubPagePlayerViewModel> Players { get; set; } = new List<ClubPagePlayerViewModel>();
    }
}
