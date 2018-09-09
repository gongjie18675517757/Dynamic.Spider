using HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamic.Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scheduler = new SchedulerFactory().CreateDefaultScheduler("https://www.cnblogs.com");
            await scheduler.Start(new WorkTask.SpiderTask("/"), 1);
        }
    }
}
