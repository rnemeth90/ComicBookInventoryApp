using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComicBookInventory.Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CharacterController(ILogger<CharacterController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory; 
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCharacters(string searchString, int? pageNumber, string sortOrder, string currentFilter)
        {
            int pageSize = 10;
            string uri = "";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["PageTitle"] = searchString + "*";
                pageNumber = 1;
                uri = $"https://localhost:5001/api/character/find-character?searchstring={searchString}";
            }
            else
            {
                ViewData["PageTitle"] = "All Characters";
                searchString = currentFilter;
                uri = "https://localhost:5001/api/character/get-all-characters";
            }

            ViewData["CurrentFilter"] = searchString;
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<CharacterViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<CharacterViewModel>>();

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(a => a.FullName);
                    break;
                default:
                    model = model.OrderBy(t => t.FullName);
                    break;
            }

            return View(PaginatedList<CharacterViewModel>.Create(model.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpGet("{id}")]
        [Route("character/")]
        public async Task<IActionResult> CharacterDetails(int id)
        {
            string uri = $"https://localhost:5001/api/character/get-character-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            CharacterViewModel? model = await response.Content
                .ReadFromJsonAsync<CharacterViewModel>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCharacter(int id)
        {
            CharacterViewModel? model = null;
            string uri = $"https://localhost:5001/api/character/get-character-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadFromJsonAsync<CharacterViewModel>();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditCharacter(CharacterViewModel model)
        {
            string uri = $"https://localhost:5001/api/character/update-character/{model.Id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var json = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await client.PatchAsync(uri, json);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllCharacters");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/character/get-character-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            CharacterViewModel? model = await response.Content
                .ReadFromJsonAsync<CharacterViewModel>();

            return View(model);
        }


        [HttpPost, ActionName("DeleteCharacter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCharacterConfirmed(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            string uri = $"https://localhost:5001/api/Character/delete-character-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");
            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllCharacters");
            }
            return RedirectToAction("Error");
        }

        // GET: Character/CreateCharacter
        [HttpGet]
        public async Task<IActionResult> CreateCharacter()
        {
            return View();
        }

        // POST: Character/CreateCharacter
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCharacter(CharacterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uri = $"https://localhost:5001/api/Character/add-character/";
                HttpClient client = _httpClientFactory.CreateClient(
                        name: "ComicBookInventory.Api");

                var postTask = await client.PostAsJsonAsync<CharacterViewModel>(uri, model);

                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllCharacters");
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
