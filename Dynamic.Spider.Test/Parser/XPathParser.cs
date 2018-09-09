using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Dynamic.Spider.Parser
{
    public class XPathParser : IParser
    {
        private HtmlDocument htmlDocument;

        public XPathParser()
        {
            htmlDocument = new HtmlDocument();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        private bool getValue(HtmlNode node, ParaseRule rule, out KeyValuePair<string, string> valuePair)
        {
            valuePair = new KeyValuePair<string, string>();
            node = node.SelectSingleNode(rule.Path);
            if (node == null)
            {
                Console.WriteLine($"未解析到{rule.Path}");
                return false;
            }
            var value = string.Empty;

            if (!string.IsNullOrWhiteSpace(rule.AttrName))
                value = node.Attributes[rule.AttrName].Value.Trim();
            else
                value = node.InnerText.Trim();

            //Console.WriteLine($"Name:{rule.Name},value:{value}");

            valuePair = new KeyValuePair<string, string>(rule.Name, value);
            return true;
        }

        private IEnumerable<KeyValuePair<string, string>> getValues(HtmlNode doc, IEnumerable<ParaseRule> rules)
        {
            foreach (var rule in rules)
            {
                var nodes = doc.SelectNodes(rule.Path);
                if (rule.Rules?.Any() == true)
                {
                    foreach (var node in nodes)
                    {
                        var values = getValues(node, rule.Rules).ToList();
                        foreach (var item in values)
                        {
                            yield return item;
                        }
                    }
                }
                else
                {
                    if (getValue(doc, rule, out var kv))
                    {
                        yield return kv;
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<string, string>> Parser(IEnumerable<ParaseRule> rules)
        {
            var doc = htmlDocument.DocumentNode;
            return getValues(doc, rules);
        }

        public void Init(string html)
        {
            htmlDocument.LoadHtml(html);
        }
    }
}
