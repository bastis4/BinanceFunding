using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class FuturesUsdtAverageFundingRate : FuturesUsdtFundingRate
    {
        public decimal AverageFundingRate { get; set; }
        public FuturesUsdtAverageFundingRate(string symbol, int period, decimal average) : base(symbol, period)
        {
            AverageFundingRate = average;
        }
    }
}
