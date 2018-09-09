using Dynamic.Spider.WorkTask;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Dynamic.Spider.Queue
{
    public class BlockingQueue : IWorkQueue
    {
        private readonly ConcurrentQueue<IWorkTask> workTasks;
        public BlockingQueue()
        {
            workTasks = new ConcurrentQueue<IWorkTask>();
        }
        public async Task<IWorkTask> DequeueAsync()
        {
            await Task.CompletedTask;
            if (workTasks.TryDequeue(out var work))
                return work;
            else
                return null;
        }

        public Task EnqueueAsync(IWorkTask workTask)
        {
            workTasks.Enqueue(workTask);
            return Task.CompletedTask;
        }
    }
}
