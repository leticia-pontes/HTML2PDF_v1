using AngleSharp;
using AngleSharp.Dom;
using HTML2PDF_v1.Parsers;
using System.Threading.Tasks;

namespace HTML2PDF_v1.Processors
{
    public class CssApplier
    {
        public async Task<IDocument> ApplyCssToHtmlAsync(string htmlContent, string cssContent)
        {
            // Create a new browsing context with CSS support
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);

            // Load the HTML content into an AngleSharp document
            var document = await context.OpenAsync(req => req.Content(htmlContent));

            // Load CSS styles using the custom CssParser
            var cssParser = new CssParser();
            var stylesheet = cssParser.ParseCss(cssContent); // Parse the CSS content

            // Create a style element
            var styleElement = document.CreateElement("style");
            styleElement.TextContent = stylesheet.ToCss(); // Convert to CSS string

            // Check if the document has a head element
            if (document.Head != null)
            {
                // Append the style element to the document head
                document.Head.AppendChild(styleElement);
            }
            else
            {
                // Se `Head` for nulo, lançar uma exceção ou adicionar o `styleElement` em outro lugar
                throw new InvalidOperationException("O documento não possui um elemento 'head'.");
            }

            // Return the styled document
            return document;
        }
    }
}
