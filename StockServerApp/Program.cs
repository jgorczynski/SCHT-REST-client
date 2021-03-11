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
            var client = new RestClient("<proper link>");
            client.Authenticator = new HttpBasicAuthenticator("<mail address>", "<password>");

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
