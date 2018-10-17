using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace SharehodlersScrapperLogic.CompanyModels
{
    public class ShareholdersTable
    {
        public readonly DataTable ShareholdersDataTable;
        public ShareholdersTable()
        {
            ShareholdersDataTable = new DataTable();
        }

        public void InitTable(HtmlDocument htmlDocument)
        {
            if (htmlDocument == null)
            {
                return;
            }
            IEnumerable<HtmlNode> shareholdersTable;
            var tables = htmlDocument.DocumentNode.SelectNodes("//table[@class='nfvtTab linkTabBl']");
            if (tables == null)
            {
                return;
            }
            try
            {
                
                shareholdersTable = tables.Where(x => x.Attributes.Count == 7)
                        .FirstOrDefault(x => string.IsNullOrEmpty(x.Attributes["style"].Value))
                        .ChildNodes.Where(x => x.EndNode.Name == "tr" && x.FirstChild.Name != "#text");
            }
            catch (NullReferenceException)
            {
                return;
            }
            
            ShareholdersDataTable.Columns.Add("", typeof(string));
            ShareholdersDataTable.Columns.Add("", typeof(double));
            ShareholdersDataTable.Columns.Add("", typeof(decimal));

            var clone = (CultureInfo)CultureInfo.GetCultureInfo("fr-FR").Clone();
            clone.NumberFormat.NumberDecimalSeparator = ",";
            foreach (var item in shareholdersTable)
            {
                var companyName = item.ChildNodes[0].InnerText.Trim();
                var equities = double.Parse(item.ChildNodes[1]
                    .InnerText
                    .Trim()
                    .Replace("-","0")
                    .Replace(",", ""), CultureInfo.GetCultureInfo("fr-FR"));
                var percents = decimal.Parse(item.ChildNodes[2]
                    .InnerText
                    .Trim()
                    .Replace("-", "0")
                    .Replace(".", ",")
                    .Replace("%", ""), clone) / 100;
                ShareholdersDataTable.Rows.Add(companyName, equities, percents);
            }
        }
    }
}
