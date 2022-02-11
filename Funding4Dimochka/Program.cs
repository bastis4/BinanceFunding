using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace Funding4Dimochka

{
    public class Program
    {
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
            var tradingSymbols = await binanceFundingClient.GetTradingSymbols();
            var resultAvgFundingRates = new List<AverageFundingRate>();
            foreach (var symbol in tradingSymbols)
            {
                resultAvgFundingRates.Add(await binanceFundingClient.GetAverageRates(symbol));
            }
            Console.WriteLine("Монета" + " " + "Среднее за 1 день" + " " + "Среднее за 7 дней" + " " + "Среднее за 30 дней");
            foreach (var item in resultAvgFundingRates)
            {
                Console.WriteLine(item.Symbol + " " + item.AvgRatePer1Day + " " + item.AvgRatePer7Days + " " + item.AvgRatePer30Days);
            }
            Console.WriteLine("Всего монет: " + resultAvgFundingRates.Count);
            Console.ReadKey();
        }
    }
}