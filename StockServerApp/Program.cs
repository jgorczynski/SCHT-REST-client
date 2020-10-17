using System;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace StockClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://stockserver20201009223011.azurewebsites.net/");
            client.Authenticator = new HttpBasicAuthenticator("01149601@pw.edu.pl", "mA4YBUw");

            //basic operations on provided server - market overview and a list of shares
            
            var stockListRequest = new RestRequest("stockexchanges", DataFormat.Json);
            var stockListJSONResponse = client.Get(stockListRequest);

            var sharesListRequest = new RestRequest("shareslist/Londyn");
            var sharesListJSONResponse = client.Get(sharesListRequest);

            string[] stockExchanges = JsonConvert.DeserializeObject<string[]>(stockListJSONResponse.Content);
            string[] sharesList = JsonConvert.DeserializeObject<string[]>(sharesListJSONResponse.Content);

            foreach (string stockExchange in stockExchanges)
                Console.WriteLine(stockExchange);

            Console.WriteLine("\nShares to buy or sell in London:");

            foreach (string share in sharesList)
                Console.WriteLine(share);

            Console.ReadKey();
        }
    }
}