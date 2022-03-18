using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace MyPathAliases
{
    class PathAliasManager
    {
        private PathAliases pathAliases;

        public IList<PathAlias> GetAllPathAliases()
        {
            return pathAliases.aliases;
        }

        public PathAlias FindAlias(string alias)
        {
            return pathAliases.aliases.Where(x => x.alias == alias).SingleOrDefault();
        }

        public void ParseJsonFile(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"File not found: {jsonPath}");
            }
            pathAliases = JsonConvert.DeserializeObject<PathAliases>(File.ReadAllText(jsonPath));
        }
    }
}
