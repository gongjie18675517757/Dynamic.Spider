using Dynamic.Spider.WorkTask;
using System.Threading.Tasks;

namespace Dynamic.Spider
{
    public interface IScheduler
    {
        string Name { get; }

        Task Start(IWorkTask workTask, int workTheardCount);
    }
}
