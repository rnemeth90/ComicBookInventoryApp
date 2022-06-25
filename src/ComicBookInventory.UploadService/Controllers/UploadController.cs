using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookInventory.UploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // can called from the user interface
        // we need a connection string or some other method of identifying the connection
        // we should get this config from somewhere else
        private readonly string _connectionStringName = "";

        // one action that uploads to azure
        public async Task<IActionResult> UploadFile([FromBody] string file)
        {

            return Ok();
        }
    }
}
