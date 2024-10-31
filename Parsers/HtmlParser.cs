using HtmlAgilityPack;

namespace HTML2PDF_v1.Parsers
{
    public class HtmlParser
    {
        public HtmlDocument LoadHtml(string htmlFilePath)
        {
            HtmlDocument document = new HtmlDocument();
            document.Load(htmlFilePath);
            return document;
        }
    }
}
