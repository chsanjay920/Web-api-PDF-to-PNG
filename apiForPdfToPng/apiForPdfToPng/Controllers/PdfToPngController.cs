using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiForPdfToPng.Controllers
{
    [Route("api/[controller]/[Name]")]
    [ApiController]
    public class PdfToPngController : ControllerBase
    {
        // GET: api/<PdfToPngController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult ToPng(IFormFile file)
        {
            byte[] pdfByteArray = null;
            try
            {
                if (file != null)
                {
                    using var memoryStream = new MemoryStream();
                    file!.CopyToAsync(memoryStream);
                    pdfByteArray = memoryStream.ToArray();
                }
                var imagesList = Freeware.Pdf2Png.ConvertAllPages(pdfByteArray);
                //foreach (var img in imagesList)
                //{
                //    using (Image image = Image.FromStream(new MemoryStream(img)))
                //    {
                //        image.Save("output.png", ImageFormat.Png);
                //    }
                //}
                return File(imagesList[0], "image/jpng");

            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }


    }
}
