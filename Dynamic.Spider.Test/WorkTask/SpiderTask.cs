using Dynamic.Spider.Downloader;
using Dynamic.Spider.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamic.Spider.WorkTask
{
    public class SpiderTask : IWorkTask
    {
        private readonly string url;

        public SpiderTask(string url)
        {
            this.url = url;
        } 

        public string Name => "采集器";

        public async Task<IEnumerable<IWorkTask>> WorkAsync(IDownloader downloader, IParserFactory parserFactory)
        {
            var html = await downloader.Down(url);
            var parser = parserFactory.CreateParser("xpath");
            parser.Init(html);
            var keyValuePairs = parser.Parser(new ParaseRule[] {
                new ParaseRule()
                {
                    Name="博客列表",
                    Path="//div[@class=\"post_item\"]",
                    Rules=new   ParaseRule[]{
                        new ParaseRule()
                        {
                            Path="*//a[@class=\"titlelnk\"]",
                            Name="标题"
                        },
                         new ParaseRule()
                        {
                            Path="*//a[@class=\"titlelnk\"]",
                            Name="链接",
                            AttrName="href"
                        }
                    }
                }
            });

            foreach (var item in keyValuePairs)
            {
                Console.WriteLine($"{item}");
            }

            var nextUrlKv = parser.Parser(new ParaseRule[] {
                new ParaseRule()
                {
                    Name="下一页",
                    Path="//div[@class=\"pager\"]/a[contains(@class,'current')]/following-sibling::a[1]",
                    AttrName="href"
                }
            });

            var results = nextUrlKv.Select(x => (IWorkTask)new SpiderTask(x.Value));


            return await Task.FromResult(results);
        }
    }
}
