using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace Funding4Dimochka

{
    public class Program
    {
        static List<FuturesUsdtAverageFundingRate> futuresUsdtAverageFundingRateList = new List<FuturesUsdtAverageFundingRate>();
        public static async Task Main()

        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(Environment.CurrentDirectory + @"\apikey.properties"))
            data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(data["API_KEY"], data["API_SECRET"])
            });
            var binanceFundingClient = new BinanceFundingClient(client);

            /*            var tradingFuturesUsdtSymbols = await client.FuturesUsdt.System.GetExchangeInfoAsync();
                        foreach (var symbol in tradingFuturesUsdtSymbols.Data.Symbols)
                        {
                            if (symbol.ContractType == ContractType.CurrentQuarter)
                            {
                                Console.WriteLine(symbol.Name);
                                Console.WriteLine(symbol.ContractType);
                                Console.WriteLine(symbol.ToString);
                                Console.WriteLine(symbol.Status);
                                Console.WriteLine(symbol.DeliveryDate);
                                Console.WriteLine(symbol.ListingDate);
                                Console.WriteLine(symbol.TriggerProtect);
                                Console.WriteLine(symbol.UnderlyingType);
                                Console.WriteLine(symbol.SettlePlan);
                            }
                        }*/

            /*            var cakkk = await client.FuturesUsdt.Market.GetFundingRatesAsync("ATAUSDT", startTime: DateTime.Now.AddDays(-1), endTime: DateTime.Now);
                        decimal r = 0M;
                        int count = 0;
                        foreach (var item in cakkk.Data)
                        {
                            r += item.FundingRate;
                            count++;
                        }
                        Console.WriteLine(r/count);

                        Console.WriteLine("-------");

                        var tradingFuturesUsdtSymbols = await binanceFundingClient.GetTradingSymbols();
                        foreach (var symbol in tradingFuturesUsdtSymbols)
                        {
                            if(symbol == "ATAUSDT")
                            {
                                futuresUsdtAverageFundingRateList.Add(await binanceFundingClient.GetAverageRates(symbol));
                            }

                        }
                        foreach (var item in futuresUsdtAverageFundingRateList)
                        {
                            Console.WriteLine(item.FutureUsdtSymbol + " " + item.AverageFundingRate);
                        }*/

            var tradingSymbols = await binanceFundingClient.GetTradingSymbols();
            foreach (var symbol in tradingSymbols)
            {
                futuresUsdtAverageFundingRateList.Add(await binanceFundingClient.GetAverageRates(symbol));
            }
            foreach (var item in futuresUsdtAverageFundingRateList)
            {
                Console.WriteLine(item.FutureUsdtSymbol + " " + item.AverageFundingRate);
            }
            Console.WriteLine(futuresUsdtAverageFundingRateList.Count);

            Console.ReadLine();
        }
    }
}