using System.IO;

namespace DracoStandardSite.Data
{
    public class PdfServices
    {
        private readonly IWebHostEnvironment _env;
        public PdfServices(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IEnumerable<string> GetPdfFiles()
        {
            var pdfFolder = Path.Combine(_env.WebRootPath, "files");
            if (!Directory.Exists(pdfFolder))
                return Enumerable.Empty<string>();

            return Directory.GetFiles(pdfFolder, "*.pdf")
                            .Select(f => Path.GetFileName(f));
        }
    }
}