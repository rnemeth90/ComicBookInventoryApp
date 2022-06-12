using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

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

        [HttpGet]
        public async Task<IActionResult> GetAllComics(string searchString)
        {
            string uri = "";
            if (!string.IsNullOrEmpty(searchString))
            {
                uri = $"https://localhost:5001/api/ComicBook/find-book?searchstring={searchString}";
            }
            else
            {
                uri = "https://localhost:5001/api/ComicBook/get-all-books";
            }

            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<ComicBookWithAuthorsAndCharactersViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<ComicBookWithAuthorsAndCharactersViewModel>>();

            return View(model);
        }

        [HttpGet("{id}")]
        [Route("comicbook/")]
        public async Task<IActionResult> ComicDetails(int id)
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

        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditComic(ComicBookViewModel model)
        {

            if (ModelState.IsValid)
            { 
                string uri = $"https://localhost:5001/api/ComicBook/update-book/{model.Id}";
                HttpClient client = _httpClientFactory.CreateClient(
                        name: "ComicbookInventory.Api");
                var json = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var result = await client.PatchAsync(uri, json);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllComics");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComic(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/ComicBook/get-book-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            ComicBookWithAuthorsAndCharactersViewModel? model = await response.Content
                .ReadFromJsonAsync<ComicBookWithAuthorsAndCharactersViewModel>();

            return View(model);
        }

        [HttpPost, ActionName("DeleteComic")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComicConfirmed(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/ComicBook/delete-comic-book-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllComics");
            }
            return RedirectToAction("Error");
        }

        // GET: ComicBook/CreateComicBook
        [HttpGet]
        public async Task<IActionResult> CreateComicBook()
        {
            ViewBag.AuthorNames = PopulateDropDownWithFullName<AuthorViewModel>("author").GetAwaiter().GetResult();
            ViewBag.CharacterNames = PopulateDropDownWithFullName<CharacterViewModel>("character").GetAwaiter().GetResult();
            return View();
        }

        // POST: ComicBook/CreateComicBook
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComicBook(ComicBookWithAuthorsAndCharactersViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uri = $"https://localhost:5001/api/comicbook/add-book/";
                HttpClient client = _httpClientFactory.CreateClient(
                        name: "ComicBookInventory.Api");

                ViewBag.AuthorNames = PopulateDropDownWithFullName<AuthorViewModel>("author").GetAwaiter().GetResult();
                ViewBag.CharacterNames = PopulateDropDownWithFullName<CharacterViewModel>("character").GetAwaiter().GetResult();
                var postTask = await client.PostAsJsonAsync<ComicBookWithAuthorsAndCharactersViewModel>(uri, model);

                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllComics");
                }
                else
                {
                    return View(model);
                }
            }
            return View();
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

        public async Task<SelectList> PopulateDropDownWithFullName<T>(string type)
        {
            string uri = $"https://localhost:5001/api/{type}/get-all-{type}s/";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicBookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<T>? entities = await response.Content
                .ReadFromJsonAsync<IEnumerable<T>>();
            return new SelectList(entities, "FullName", "FullName");
        }
    }
}
