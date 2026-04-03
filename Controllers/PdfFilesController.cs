using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class PdfFilesController : ControllerBase
{
    private readonly string _pdfDirectory = "wwwroot/pdfs";

    [HttpGet]
    public IActionResult GetPdfFiles()
    {
        if (!Directory.Exists(_pdfDirectory))
            return Ok(new string[0]);

        var files = Directory.GetFiles(_pdfDirectory, "*.pdf")
            .Select(Path.GetFileName)
            .ToArray();

        return Ok(files);
    }
}