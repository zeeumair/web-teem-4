using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Models;

namespace Webshop
{
    public class CurrencyManager
    {
        public static async Task<CurrencyRates> GetCurrencyRates()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("https://api.exchangeratesapi.io/latest?base=SEK");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                return JsonConvert.DeserializeObject<CurrencyRates>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
