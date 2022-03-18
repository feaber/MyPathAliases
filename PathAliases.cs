using System.Collections.Generic;

namespace MyPathAliases
{
    public class PathAlias
    {
        public string alias { get; set; }
        public string path { get; set; }
    }

    public class PathAliases
    {
        public IList<PathAlias> aliases { get; set; }
    }
}
