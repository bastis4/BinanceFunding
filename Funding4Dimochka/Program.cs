using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace Funding4Dimochka

{
    public class Program
    {

        static DateTime month = DateTime.Now.AddDays(-30);
        static DateTime week = DateTime.Now.AddDays(-7);
        static DateTime day = DateTime.Now.AddDays(-1);
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

            var tradingFuturesUsdtSymbols = await binanceFundingClient.GetTradingSymbols();
            foreach (var symbol in tradingFuturesUsdtSymbols)
            {
                futuresUsdtAverageFundingRateList.Add(await binanceFundingClient.GetRates(symbol, week));
            }
            foreach (var item in futuresUsdtAverageFundingRateList)
            {
                Console.WriteLine(item.FutureUsdtSymbol + " " + item.AverageFundingRate + " " + item.HistoryPeriod);
            }
            Console.WriteLine(futuresUsdtAverageFundingRateList.Count);

            Console.ReadKey();
        }
    }
}