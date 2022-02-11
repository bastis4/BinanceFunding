using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class BinanceFundingClient
    {
        private BinanceClient _binanceClient;
        public BinanceFundingClient(BinanceClient client)
        {
            _binanceClient = client;
        }

        public async Task<List<string>> GetTradingSymbols()
        {
            var tradingSymbols = new List<string>();
            var callResult = await _binanceClient.FuturesUsdt.System.GetExchangeInfoAsync();
            foreach (var item in callResult.Data.Symbols)
            {
                if (item.Status == SymbolStatus.Trading && item.ContractType != ContractType.CurrentQuarter)
                {
                    tradingSymbols.Add(item.Name);
                }
            }
            return tradingSymbols;
        }
        public async Task<FuturesUsdtAverageFundingRate> GetAverageRates(string symbol)
        {
            var futuresUsdtFundigRatesList = new List<FuturesUsdtFundingRate>();
            var callResult = await _binanceClient.FuturesUsdt.Market.GetFundingRatesAsync(symbol, startTime: DateTime.Now.AddDays(-1), endTime: DateTime.Now);
            var averageFundingRate = callResult.Data.Average(x => x.FundingRate);

            FuturesUsdtAverageFundingRate futuresUsdtAverageFundingRate = new FuturesUsdtAverageFundingRate(symbol, averageFundingRate);
            return futuresUsdtAverageFundingRate;
        }
    }
}
