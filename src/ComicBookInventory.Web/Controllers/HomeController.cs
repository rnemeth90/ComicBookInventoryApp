using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetAllComics()
        {
            string uri = "https://localhost:5001/api/ComicBook/get-all-books";

            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<ComicBookWithAuthorsAndCharactersViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<ComicBookWithAuthorsAndCharactersViewModel>>();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}