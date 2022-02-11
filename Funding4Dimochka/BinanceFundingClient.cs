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
            var tradingFuturesUsdtSymbols = new List<string>();
            var callResult = await _binanceClient.FuturesUsdt.System.GetExchangeInfoAsync();
            foreach (var item in callResult.Data.Symbols)
            {
                if (item.Status == SymbolStatus.Trading)
                {
                    tradingFuturesUsdtSymbols.Add(item.Name);
                }
            }
            return tradingFuturesUsdtSymbols;
        }
        public async Task<FuturesUsdtAverageFundingRate> GetRates(string symbol, DateTime startTime)
        {
            var futuresUsdtFundigRatesList = new List<FuturesUsdtFundingRate>();
            FuturesUsdtFundingRate futuresUsdtFundingRate;
            var callResult = await _binanceClient.FuturesUsdt.Market.GetFundingRatesAsync(symbol, startTime: startTime, endTime: DateTime.Now);
            var period = DateTime.Now.Day - startTime.Day;
            foreach (var item in callResult.Data)
            {
                futuresUsdtFundingRate = new FuturesUsdtFundingRate(symbol, item.FundingRate, period);
                futuresUsdtFundigRatesList.Add(futuresUsdtFundingRate);
            }
            var averageFundingRate = 0M;
            if (futuresUsdtFundigRatesList != null && futuresUsdtFundigRatesList.Count > 0)
            {
                averageFundingRate = GetAverageFundingRate(futuresUsdtFundigRatesList);
            }
            FuturesUsdtAverageFundingRate futuresUsdtAverageFundingRate = new FuturesUsdtAverageFundingRate(symbol, period, averageFundingRate);
            return futuresUsdtAverageFundingRate;
        }
        public decimal GetAverageFundingRate(List<FuturesUsdtFundingRate> ratePerSymbolAndPeriodlList)
        {
            var totalFundingRatesPerPeriod = 0M;
            foreach (var futuresUsdtFundingRate in ratePerSymbolAndPeriodlList)
            {
                totalFundingRatesPerPeriod += futuresUsdtFundingRate.FundingRate;
            }
            var averageFundingRatePerPeriod = totalFundingRatesPerPeriod / ratePerSymbolAndPeriodlList.Count;
            return averageFundingRatePerPeriod;
        }
    }
}
