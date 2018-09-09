using System.Collections.Generic;

namespace Dynamic.Spider
{
    public class ParaseRule
    {
        public string Name { get; set; } 

        public string Path { get; set; }

        public string AttrName { get; set; }

        public IEnumerable<ParaseRule> Rules { get; set; }
    }
}
