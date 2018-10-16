﻿using HtmlAgilityPack;

namespace SharehodlersScrapperLogic.ShareholderModels
{
    public class SummaryText
    {
        HtmlNode node;
        public string Text;
        public SummaryText(HtmlNode HtmlNode)
        {
            node = HtmlNode;
            Text = node.InnerText.ToUpper();
        }
    }
}
