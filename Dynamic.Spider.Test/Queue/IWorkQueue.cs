using Dynamic.Spider.WorkTask;
using System.Threading.Tasks;

namespace Dynamic.Spider.Queue
{
    public interface IWorkQueue
    {
        Task<IWorkTask> DequeueAsync();

        Task EnqueueAsync(IWorkTask workTask); 
    }
}
