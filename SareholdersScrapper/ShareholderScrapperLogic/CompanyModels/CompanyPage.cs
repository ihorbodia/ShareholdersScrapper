using HtmlAgilityPack;

namespace SharehodlersScrapperLogic.CompanyModels
{
    public class CompanyPage
    {
        HtmlDocument htmlDocument;
        public CompanyPage(HtmlDocument htmlDocument)
        {

        }

        public string BusinessSummary { get; set; }
    }
}
