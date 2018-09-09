using System.Threading.Tasks;

namespace Dynamic.Spider.WorkTask
{
    public interface IWorkTaskFactory
    {
        Task<IWorkTask> GetWorkTask(string taskId);
    }
}
