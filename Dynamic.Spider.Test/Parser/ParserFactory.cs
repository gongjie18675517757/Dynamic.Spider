using System;
using System.Threading.Tasks;

namespace Dynamic.Spider.Parser
{
    public class ParserFactory : IParserFactory
    {
        public IParser CreateParser(string name)
        {
            return new XPathParser();
        }
    }
}
