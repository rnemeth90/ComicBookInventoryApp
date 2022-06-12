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
        public async Task<IActionResult> GetAllCharacters(string searchString)
        {
            string uri = "";
            if (!string.IsNullOrEmpty(searchString))
            {
                uri = "https://localhost:5001/api/character/get-all-characters";
            }
            else
            {
                uri = $"https://localhost:5001/api/character/get-all-characters?searchstring={searchString}";
            }


            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            IEnumerable<CharacterViewModel>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<CharacterViewModel>>();

            return View(model);
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
            string uri = $"https://localhost:5001/api/character/delete-character-by-id/{id}";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicbookInventory.Api");

            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllCharacters");
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> CreateCharacter(CharacterViewModel model)
        {
            string uri = $"https://localhost:5001/api/Character/add-character/";
            HttpClient client = _httpClientFactory.CreateClient(
                    name: "ComicBookInventory.Api");

            var postTask = client.PostAsJsonAsync<CharacterViewModel>(uri, model);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllCharacters");
            }
            else
            {
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}
