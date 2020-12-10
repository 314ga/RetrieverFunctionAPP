using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RetrieverFunctionApp
{
    public static class DataRetriever
    {
        static HttpClient client = new HttpClient();
        [FunctionName("DataRetriever")]
        public static async Task RunAsync([TimerTrigger("10,50 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                // Call Your  API
                HttpClient newClient = new HttpClient();
                HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, "https://sep6-ua-weather.azurewebsites.net/manufacturer?requestBody=planes-per-manufacturer");
                //Read Server Response
                HttpResponseMessage response = await newClient.SendAsync(newRequest);
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
           
        }

    }
}
