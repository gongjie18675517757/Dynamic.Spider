using System;
using System.Threading.Tasks;

namespace Dynamic.Spider.WorkTask
{
    public class WorkTaskFactory : IWorkTaskFactory
    {
        public Task<IWorkTask> GetWorkTask(string taskId)
        {
            throw new NotImplementedException();
        }
    } 
}
