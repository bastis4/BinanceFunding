using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FundingGetter
{
    public class Program
    {
        static string APIKEY = "KAXy7cEVLYf4ZYQzGmFMgX6kXXlFK9nodsE7eKVarIeruurCI0R6W0CIrCLN12hh";
        static string APISECRET = "ijPy4QI35rduToSiw8e6G1xVSRg6QWcJ9UOYuadk8LryM78lr0qyrLNafM6fthl3";
        static DateTime date1 = new DateTime(2022,02,07);
        static DateTime date2 = new DateTime(2022,02,08);
        public static async Task Main()
        {
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(APIKEY, APISECRET)

            });
            var callResult = await client.FuturesCoin.Market.GetFundingRatesAsync("BTCUSDT", date1, date2, 100);
                //client.FuturesCoin.Market.GetFundingRatesAsync("BTCUSDT");client.FuturesCoin.GetPositionInformationAsync()
            // Make sure to check if the call was successful

            Console.WriteLine(callResult);
            /*if (!callResult.Success)
            {
                Console.WriteLine("Error");
            }
            else
            {
                var lcr = callResult.Data.ToArray();// Call succeeded, callResult.Data will have the resulting data
                foreach (var item in lcr)
                {
                    Console.WriteLine(item);
                }
                
            }*/
            Console.ReadKey();
        }

    }
}