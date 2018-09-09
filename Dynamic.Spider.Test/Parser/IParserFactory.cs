using System.Threading.Tasks;

namespace Dynamic.Spider.Parser
{
    public interface IParserFactory
    {
        IParser CreateParser(string name);
    }
}
