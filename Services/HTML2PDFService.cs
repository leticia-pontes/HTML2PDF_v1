using HTML2PDF_v1.Parsers;
using HTML2PDF_v1.Processors;
using System.IO;
using System.Threading.Tasks;

namespace HTML2PDF_v1.Services
{
    public class HTML2PDF_v1Service
    {
        private readonly HtmlParser _htmlParser;
        private readonly CssParser _cssParser;
        private readonly CssApplier _cssApplier;
        private readonly PdfGenerator _pdfGenerator;

        public HTML2PDF_v1Service()
        {
            _htmlParser = new HtmlParser();
            _cssParser = new CssParser();
            _cssApplier = new CssApplier();
            _pdfGenerator = new PdfGenerator();
        }

        // Aceita conteúdo HTML como string
        public async Task ConvertAsync(string htmlContent, string cssContent, Stream outputStream)
        {
            // Aplica CSS ao HTML
            var styledDocument = await _cssApplier.ApplyCssToHtmlAsync(htmlContent, cssContent);

            // Gera o PDF no stream de saída
            using (var memoryStream = new MemoryStream())
            {
                // Chama o método de conversão, adaptado para aceitar um Stream
                _pdfGenerator.ConvertHtmlToPdf(memoryStream, styledDocument);

                // Reseta a posição do stream antes de retornar
                memoryStream.Position = 0;

                // Copia o conteúdo do memoryStream para o outputStream
                await memoryStream.CopyToAsync(outputStream);
            }
        }
    }
}
