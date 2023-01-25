using CryptoPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace CryptoPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string baseURL = "https://api.coingecko.com/api/v3/";
        HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IList<CoinEntity> coin = new List<CoinEntity>();
            var results = await GetCoinMarketList("usd");
            coin = JsonConvert.DeserializeObject<List<CoinEntity>>(results);

            ViewData.Model = coin;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET : https://api.coingecko.com/api/v3/coins/markets?vs_currrency={currency}
        // returns results as string
        public async Task<string> GetCoinMarketList(string currency)
        {
            try
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("coins/markets?vs_currency=" + currency);
             
                return getData.Content.ReadAsStringAsync().Result;
            } catch (Exception ex)
            {
                return ex.ToString();
            }

            

        }
    }
}