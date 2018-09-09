using System.Collections.Generic;

namespace Dynamic.Spider.Parser
{
    public interface IParser
    {  
        string Name { get; }

        void Init(string html);

        IEnumerable<KeyValuePair<string, string>> Parser(IEnumerable<ParaseRule> paraceRule);
    }
}
