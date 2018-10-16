using HtmlAgilityPack;

namespace SharehodlersScrapperLogic.CompanyModels
{
    public class ShareholdersTable
    {
        HtmlNode document;
        public string Name { get; set; }
        public string Equitities { get; set; }
        public string Percent { get; set; }

        public ShareholdersTable(HtmlNode htmlNode)
        {
            document = htmlNode;
        }
    }
}
