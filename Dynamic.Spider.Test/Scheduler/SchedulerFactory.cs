using Dynamic.Spider.Downloader;
using Dynamic.Spider.Parser;
using Dynamic.Spider.Queue;
using Dynamic.Spider.WorkTask;

namespace Dynamic.Spider
{
    public class SchedulerFactory : ISchedulerFactory
    {
        public  IScheduler CreateDefaultScheduler(string baseUrl)
        {
            return new DefaultScheduler("博客园", 
                new WorkTaskFactory(), 
                new HtmlDownloader(baseUrl), 
                new ParserFactory(), 
                new BlockingQueue());
        }
    }
}
