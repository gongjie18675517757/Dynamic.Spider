using Dynamic.Spider.Downloader;
using Dynamic.Spider.Parser;
using Dynamic.Spider.Queue;
using Dynamic.Spider.WorkTask;
using System;
using System.Threading.Tasks;

namespace Dynamic.Spider
{
    public class DefaultScheduler : IScheduler
    {
        private readonly IWorkTaskFactory workTaskFactory;
        private readonly IDownloader downloader;
        private readonly IParserFactory parserFactory;
        private readonly IWorkQueue workQueue;
        private Task[] tasks;
        public DefaultScheduler(string name, IWorkTaskFactory workTaskFactory, IDownloader downloader, IParserFactory parserFactory, IWorkQueue workQueue)
        {
            Name = name;
            this.workTaskFactory = workTaskFactory;
            this.downloader = downloader;
            this.parserFactory = parserFactory;
            this.workQueue = workQueue;
        }

        public string Name { get; }

        public async Task Start(IWorkTask workTask, int workTheardCount)
        {
            await workQueue.EnqueueAsync(workTask);

            if (workTheardCount < 1)
                workTheardCount = 1;

            tasks = new Task[workTheardCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Work();
            }
            await Task.WhenAll(tasks);
        }

        private async Task Work()
        {
            while (true)
            {
                var workTask = await workQueue.DequeueAsync();
                if (workTask == null)
                    continue;
                var nextTasks = await workTask.WorkAsync(downloader, parserFactory);
                foreach (var task in nextTasks)
                {
                    await workQueue.EnqueueAsync(task);
                }
            }
        }
    }
}
