using Binance.Net;
using Binance.Net.Enums;

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
        public async Task<AverageFundingRate> GetAverageRates(string symbol)
        {
            var callResultPer30Days = await _binanceClient.FuturesUsdt.Market.GetFundingRatesAsync(symbol, startTime: DateTime.Now.AddDays(-30), endTime: DateTime.Now);
            
            var avgPer30Days = callResultPer30Days.Data.Average(x => x.FundingRate);
            var avgPer7Days = callResultPer30Days.Data
                .Where(x => x.FundingTime >= DateTime.Now.AddDays(-7))
                .Average(x => x.FundingRate);
            var avgPer1Day = callResultPer30Days.Data
                .Where(x => x.FundingTime >= DateTime.Now.AddDays(-1))
                .Average(x => x.FundingRate);

            var averageFundingRate = new AverageFundingRate();
            {
                averageFundingRate.Symbol = symbol;
                averageFundingRate.AvgRatePer1Day = avgPer1Day;
                averageFundingRate.AvgRatePer7Days = avgPer7Days;
                averageFundingRate.AvgRatePer30Days = avgPer30Days;
            }

            return averageFundingRate;
        }
    }
}
