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
        public async Task<IActionResult> GetAllAuthors(string searchString, int? pageNumber, string sortOrder, string currentFilter)
        {
            int pageSize = 10;
            string uri = "";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["PageTitle"] = searchString + "*";
                pageNumber = 1;
                uri = $"https://localhost:5001/api/Author/find-author?searchstring={searchString}";
            }
            else
            {
                ViewData["PageTitle"] = "All Authors";
                searchString = currentFilter;
                uri = "https://localhost:5001/api/Author/get-all-authors";
            }

            ViewData["CurrentFilter"] = searchString;
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<AuthorViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<AuthorViewModel>>();

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(a => a.FullName);
                    break;
                default:
                    model = model.OrderBy(t => t.FullName);
                    break;
            }

            return View(PaginatedList<AuthorViewModel>.Create(model.AsQueryable(), pageNumber ?? 1, pageSize));
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
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/author/get-author-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            AuthorViewModel? model = await response.Content
                .ReadFromJsonAsync<AuthorViewModel>();

            return View(model);
        }

        [HttpPost, ActionName("DeleteAuthor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAuthorConfirmed(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/author/delete-author-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllAuthors");
            }
            return RedirectToAction("Error");
        }

        // GET: Character/CreateCharacter
        [HttpGet]
        public async Task<IActionResult> CreateAuthor()
        {
            return View();
        }

        // POST: Character/CreateCharacter
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uri = $"https://localhost:5001/api/author/add-author/";
                HttpClient client = _httpClientFactory.CreateClient(
                        name: "ComicBookInventory.Api");

                var postTask = await client.PostAsJsonAsync<AuthorViewModel>(uri, model);

                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllAuthors");
                }
                else
                {
                    return View(model);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}
