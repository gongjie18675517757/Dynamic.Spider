namespace Dynamic.Spider
{
    public interface ISchedulerFactory
    {
        IScheduler CreateDefaultScheduler(string baseUrl);
    }
}
