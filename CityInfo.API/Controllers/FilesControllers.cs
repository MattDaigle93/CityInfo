using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesControllers : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesControllers(
            FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
           _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(
                    nameof(fileExtensionContentTypeProvider));
        }
        [HttpGet("{fileId")]
        public ActionResult GetFile(string fileId)
        {
            // look up the actual file, depnding on the fileId...
            // demo code
            var pathToFile = "demo";

            // check whether the file exists
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }
            
            if (!_fileExtensionContentTypeProvider.TryGetContentType(
                pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllText(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
