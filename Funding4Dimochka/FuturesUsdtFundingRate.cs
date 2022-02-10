using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class FuturesUsdtFundingRate
    {
        public string futureUsdtSymbol { get; set; }
        public decimal fundingRate { get; set; }
        public int historyPeriod { get; set; } 

        public FuturesUsdtFundingRate(string symbol, decimal funding, int period)
        {
            futureUsdtSymbol = symbol;
            fundingRate = funding;
            historyPeriod = period;
        }

        public FuturesUsdtFundingRate(string symbol, int period)
        {
            futureUsdtSymbol = symbol;
            historyPeriod = period;
        }

    }
}
