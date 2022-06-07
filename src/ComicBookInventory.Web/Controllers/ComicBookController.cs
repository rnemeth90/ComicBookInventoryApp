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
        public async Task<ActionResult> EditComic(ComicBookViewModel model)
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

        public async Task<IActionResult> CreateComicBook(ComicBookWithAuthorsAndCharactersViewModel model)
        {
            string uri = $"https://localhost:5001/api/comicbook/add-book/";
            string authorUri = $"https://localhost:5001/api/authors/get-all-authors/";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicBookInventory.Api");


            var authorRequest = new HttpRequestMessage(HttpMethod.Get, authorUri);
            var authorResponse = await client.SendAsync(authorRequest);
            IEnumerable<AuthorViewModel>? authors = await authorResponse.Content
                .ReadFromJsonAsync<IEnumerable<AuthorViewModel>>();
            ViewBag.AuthorNames = new SelectList(authors, "Id", "FullName");

            //model.AuthorNames = Request.Form["Authors"].ToList();

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

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }

        private async Task<IEnumerable<SelectListItem>> PopulateAuthorDropDownList(IEnumerable<AuthorViewModel> authors)
        {
            var selectList = new List<SelectListItem>();
            foreach (var a in authors)
            {
                selectList.Add(new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                });
            }
            return selectList;
        }
    }
}
