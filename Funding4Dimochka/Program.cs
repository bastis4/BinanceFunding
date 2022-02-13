using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Spectre.Console;
using System.Text;
using System.Globalization;


namespace Funding4Dimochka

{
    public class Program
    {
        public static async Task Main()

        {
            var dataCredentials = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(Environment.CurrentDirectory + @"\apikey.properties"))
                dataCredentials.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(dataCredentials["API_KEY"], dataCredentials["API_SECRET"])
            });
            var binanceFundingClient = new BinanceFundingClient(client);
            var tradingSymbols = await binanceFundingClient.GetTradingSymbols();
            var resultAvgFundingRates = new List<AverageFundingRate>();
            
            #region drawing table
            var table = new Table().Centered();
            table.Border(TableBorder.MinimalDoubleHead);
            var setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 5;
            await AnsiConsole.Live(table)
                 .StartAsync(async ctx =>
                 {
                     table.AddColumn("[aquamarine1]Монета[/]").Centered();
                     table.AddColumn("[deeppink2]Среднее за [darkolivegreen1]1[/] день[/]").Centered();
                     table.AddColumn("[deeppink2]Среднее за [darkolivegreen1]7[/] дней[/]").Centered();
                     table.AddColumn("[deeppink2]Среднее за [darkolivegreen1]30[/] дней[/]").Centered();
                     foreach (var symbol in tradingSymbols)
                     {
                         var result = await binanceFundingClient.GetAverageRates(symbol);
                         table.AddRow(
                             new Markup(result.Symbol.ToString()), 
                             new Markup(result.AvgRatePer1Day.ToString("N",setPrecision)), 
                             new Markup(result.AvgRatePer7Days.ToString("N", setPrecision)), 
                             new Markup(result.AvgRatePer30Days.ToString("N", setPrecision)));
                         resultAvgFundingRates.Add(result);
                         ctx.Refresh();
                     }
                     table.AddRow(new Panel("[cyan2]Всего монет: [/]" + resultAvgFundingRates.Count));
                 });
            #endregion
            Console.ReadKey();
        }

    }
}