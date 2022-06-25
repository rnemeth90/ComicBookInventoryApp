using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookInventory.UploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        // we should store the files locally on a disk or in memory

        // one action that downloads from azure
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            return Ok();
        }
    }
}
