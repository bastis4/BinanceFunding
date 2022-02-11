using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class FuturesUsdtFundingRate
    {
        public string FutureUsdtSymbol { get; set; }
        public decimal FundingRate { get; set; }

        public FuturesUsdtFundingRate(string symbol, decimal funding)
        {
            FutureUsdtSymbol = symbol;
            FundingRate = funding;
        }

        public FuturesUsdtFundingRate(string symbol)
        {
            FutureUsdtSymbol = symbol;
        }

    }
}
