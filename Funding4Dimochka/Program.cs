﻿using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.Logging;
using System.Numerics;
using System.Reflection;

namespace Funding4Dimochka

{
    public class Program
    {
        static string APIKEY = "KAXy7cEVLYf4ZYQzGmFMgX6kXXlFK9nodsE7eKVarIeruurCI0R6W0CIrCLN12hh";
        static string APISECRET = "ijPy4QI35rduToSiw8e6G1xVSRg6QWcJ9UOYuadk8LryM78lr0qyrLNafM6fthl3";
        static DateTime month = DateTime.Now.AddDays(-30);
        static DateTime week = DateTime.Now.AddDays(-7);
        static DateTime day = DateTime.Now.AddDays(-1);
        
        static List<FuturesUsdtAverageFundingRate> futuresUsdtAverageFundingRateList = new List<FuturesUsdtAverageFundingRate>();
        public static async Task Main()

        {

            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(APIKEY, APISECRET)

            });

            var tradingFuturesUsdtSymbols = await GetTradingFuturesUsdtSymbols(client);
            foreach (var symbol in tradingFuturesUsdtSymbols)
            {
                futuresUsdtAverageFundingRateList.Add(await GetTradingFuturesUsdtFundingRates(client, symbol, week));
            }
            foreach (var item in futuresUsdtAverageFundingRateList)
            {
                Console.WriteLine(item.futureUsdtSymbol + " " + item.averageFundingRate + " " + item.historyPeriod);
            }
            Console.WriteLine(futuresUsdtAverageFundingRateList.Count);

            Console.ReadKey();
        }

        #region Methods

        public static async Task<List<string>> GetTradingFuturesUsdtSymbols(BinanceClient client)
        {
           var tradingFuturesUsdtSymbols = new List<string>();
           var callResult = await client.FuturesUsdt.System.GetExchangeInfoAsync();
           foreach (var item in callResult.Data.Symbols)
            {
                if (item.Status == SymbolStatus.Trading)
                {
                    tradingFuturesUsdtSymbols.Add(item.Name);
                }
            }
           return tradingFuturesUsdtSymbols;
        }

        public static async Task<FuturesUsdtAverageFundingRate> GetTradingFuturesUsdtFundingRates(BinanceClient client, string symbol, DateTime startTime)
        {
            var futuresUsdtFundigRatesList = new List<FuturesUsdtFundingRate>();
            FuturesUsdtFundingRate futuresUsdtFundingRate;
            var callResult = await client.FuturesUsdt.Market.GetFundingRatesAsync(symbol, startTime: startTime, endTime: DateTime.Now);
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

        public static decimal GetAverageFundingRate(List<FuturesUsdtFundingRate> ratePerSymbolAndPeriodlList)
        {
            var totalFundingRatesPerPeriod = 0M;
            foreach (var futuresUsdtFundingRate in ratePerSymbolAndPeriodlList)
            {
                totalFundingRatesPerPeriod += futuresUsdtFundingRate.fundingRate;
            }
            var averageFundingRatePerPeriod = totalFundingRatesPerPeriod / ratePerSymbolAndPeriodlList.Count;
            return averageFundingRatePerPeriod;
        }

        #endregion

    }
}