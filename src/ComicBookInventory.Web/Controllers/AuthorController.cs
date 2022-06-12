using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComicBookInventory.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<ComicBookController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorController(ILogger<ComicBookController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors(string searchString)
        {
            string uri = "";
            if (!string.IsNullOrEmpty(searchString))
            {
                uri = $"https://localhost:5001/api/Author/find-author?searchstring={searchString}";
            }
            else
            {
                uri = "https://localhost:5001/api/Author/get-all-authors";
            }

            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<AuthorViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<AuthorViewModel>>();

            return View(model);
        }

        [HttpGet("{id}")]
        [Route("author/")]
        public async Task<IActionResult> AuthorDetails(int id)
        {
            string uri = $"https://localhost:5001/api/Author/get-author-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            AuthorViewModel? model = await response.Content
                .ReadFromJsonAsync<AuthorViewModel>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            AuthorViewModel? model = null;
            string uri = $"https://localhost:5001/api/Author/get-author-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadFromJsonAsync<AuthorViewModel>();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditAuthor(AuthorViewModel model)
        {
            string uri = $"https://localhost:5001/api/Author/update-author-by-id/{model.Id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var json = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await client.PatchAsync(uri, json);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllAuthors");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            string uri = $"https://localhost:5001/api/Author/delete-author-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllAuthors");
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> CreateAuthor(AuthorViewModel model)
        {
            string uri = $"https://localhost:5001/api/Author/add-author/";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicBookInventory.Api");

            var postTask = client.PostAsJsonAsync<AuthorViewModel>(uri, model);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllAuthors");
            }
            else
            {
                return View (model);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}
