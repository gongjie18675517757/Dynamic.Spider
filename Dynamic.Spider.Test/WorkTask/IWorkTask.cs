using Dynamic.Spider.Downloader;
using Dynamic.Spider.Parser;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynamic.Spider.WorkTask
{
    public interface IWorkTask
    {  
        string Name { get; }

        Task<IEnumerable<IWorkTask>> WorkAsync(IDownloader downloader, IParserFactory parserFactory);
    }
}
