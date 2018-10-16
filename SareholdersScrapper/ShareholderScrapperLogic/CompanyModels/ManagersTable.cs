using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharehodlersScrapperLogic.CompanyModels
{
    public class ManagersTable
    {
        private HtmlNode document;
        IEnumerable<CurrentPositionTableItem> Rows;

        public ManagersTable()
        {

        }


        public class ManagersTableItem
        {
            public string Name { get; set; }
            public int? Age { get; set; }
            public int? Since { get; set; }
            public string Title { get; set; }
            public ManagersTableItem()
            {

            }
        }
    }
}
