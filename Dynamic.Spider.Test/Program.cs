using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dynamic.Spider.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Group group = new Group()
            {
                Name = "博客圆",
                StartUrl = "https://www.cnblogs.com/AllBloggers.aspx",
                Steps = new List<Step>() {
                    new Step()
                    {
                        Name="用户列表",
                        Order=0,
                        Parser=StepParser.XPath,
                        ThreadCount=1,
                        Sleep=0,
                        Fileds=new Filed[]{
                            new Filed(){
                                Expression="//table[1]",
                                Name="ALL USER",
                                Children=new Filed[]{
                                    new Filed()
                                    {
                                        Expression="//td/a[1]",
                                        Name="USER"
                                    }
                                }
                            }
                        },
                    },
                },

            };
            SpiderBootstrap.Start(group);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 爬虫启动器
    /// </summary>
    class SpiderBootstrap
    {
        public static async void Start(Group group)
        {
            Spider spider = new Spider(group);
            await spider.StartAsync();
        }

        class Spider
        {
            private readonly Group group;
            private readonly HttpClient httpClient;

            public Spider(Group group)
            {
                this.group = group;
                this.httpClient = new HttpClient();
            }

            void log(object o) => Console.WriteLine($"{DateTime.Now}\t{o}");

            public void Parase()
            {

            }
            public async Task StartAsync()
            {
                var html = await httpClient.GetStringAsync(group.StartUrl);

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                foreach (var step in group.Steps)
                {
                    foreach (var field in step.Fileds)
                    {
                        var node = doc.DocumentNode.SelectSingleNode(field.Expression);
                        log(node.InnerText.Trim());
                        foreach (var child in field.Children)
                        {
                            var c_node = node.SelectNodes(child.Expression);
                            foreach (var item in c_node)
                            {
                                log(item.InnerText.Trim());
                            }
                        }
                    }
                }
            }
        }
    }




    /// <summary>
    /// 爬虫组
    /// </summary>
    class Group
    {
        /// <summary>
        /// 爬虫名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 起始页
        /// </summary>
        public string StartUrl { get; set; }

        /// <summary>
        /// 采集步骤
        /// </summary>
        public IEnumerable<Step> Steps { get; set; }
    }

    /// <summary>
    /// 文本解析器
    /// </summary>
    enum StepParser
    {
        XPath = 0,
        CSS = 1,
        JSON = 2,
    }

    /// <summary>
    /// 爬虫步骤
    /// </summary>
    class Step
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 解析器名称
        /// </summary>
        public StepParser Parser { get; set; }

        /// <summary>
        /// 线程数量
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// 采集间隔[ms]
        /// </summary>
        public int Sleep { get; set; }

        /// <summary>
        /// 要采集的字段
        /// </summary>
        public IEnumerable<Filed> Fileds { get; set; }

        /// <summary>
        /// 后面的步骤
        /// </summary>
        public IEnumerable<Step> Next { get; set; }
    }

    /// <summary>
    /// 采集实体
    /// </summary>
    class Model
    {

        /// <summary>
        /// 解析器
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 所有字段
        /// </summary>
        public IEnumerable<Filed> Fileds { get; set; }
    }

    /// <summary>
    /// 要采集的字段
    /// </summary>
    class Filed
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 子字段
        /// </summary>
        public IEnumerable<Filed> Children { get; set; } = new Filed[0];
    }
}
