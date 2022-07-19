using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComicBookInventory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            string uri = "https://localhost:5001/api/ComicBook/get-all-books";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = client.Send(request);
            var results = response.Content.ReadFromJsonAsync<IEnumerable<ComicBookWithAuthorsAndCharactersViewModel>>()
                .GetAwaiter().GetResult();
            HomeIndexViewModel? model = new
            (
                visitorCount: 0,
                comicBooks: results.ToList()
            );
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}