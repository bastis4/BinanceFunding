using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class AverageFundingRate
    {
        public string Symbol { get; set; }
        public decimal AvgRatePer1Day { get; set; }
        public decimal AvgRatePer7Days { get; set; }
        public decimal AvgRatePer30Days { get; set; }

    }
}
