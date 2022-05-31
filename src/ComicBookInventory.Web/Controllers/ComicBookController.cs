using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComicBookInventory.Web.Controllers
{
    public class ComicBookController : Controller
    {
        private readonly ILogger<ComicBookController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ComicBookController(ILogger<ComicBookController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComicById(int id)
        {
            string uri = $"https://localhost:5001/api/ComicBook/get-book-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            ComicBookWithAuthorsAndCharactersViewModel? model = await response.Content
                .ReadFromJsonAsync<ComicBookWithAuthorsAndCharactersViewModel>();

            return View(model);
        }

        public async Task<IActionResult> EditComic(int id)
        {
            ComicBookViewModel? model = null;
            string uri = $"https://localhost:5001/api/ComicBook/get-book-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadFromJsonAsync<ComicBookViewModel>();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditComic(ComicBookViewModel model)
        {
            string uri = $"https://localhost:5001/api/ComicBook/update-book/{model.Id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var json = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await client.PatchAsync(uri, json);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllComics");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComic(int id)
        {
            string uri = $"https://localhost:5001/api/ComicBook/delete-comic-book-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            //var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllComics");
            }
            return RedirectToAction("Error");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}
