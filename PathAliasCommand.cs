using System.Management.Automation;
using System.IO;
using System;

namespace MyPathAliases
{
    [Cmdlet(VerbsCommon.Get, "PathAlias")]
    public class PathAliasCommand : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }
        private string alias = null;

        [Parameter(Mandatory = false)]
        public string AliasesJsonPath
        {
            get { return jsonPath; }
            set { jsonPath = value; }
        }
        private string jsonPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter PrintAll
        {
            get { return printAll; }
            set { printAll = value; }
        }
        private bool printAll = false;

        private PathAliasManager pathAliasManager;

        protected override void BeginProcessing()
        {
            pathAliasManager = new PathAliasManager();
            if (string.IsNullOrEmpty(jsonPath))
            {
                jsonPath = Environment.GetEnvironmentVariable("PATH_ALIAS_JSON_PATH");
            }
        }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(jsonPath))
            {
                WriteObject("Please provide json path by AliasesJsonPath parameter or PATH_ALIAS_JSON_PATH env variable");
                return;
            }

            try
            {
                pathAliasManager.ParseJsonFile(jsonPath);
            }
            catch(FileNotFoundException e)
            {
                WriteObject(e.Message);
                return;
            }

            if (string.IsNullOrEmpty(alias) && !printAll)
            {
                WriteObject("You must provide at least one of parapeters: Alias, PrintAll");
                return;
            } 
            else
            {
                if (printAll)
                {
                    WriteObject(pathAliasManager.GetAllPathAliases());
                }

                if (!string.IsNullOrEmpty(alias))
                {
                    var aliasPath = pathAliasManager.FindAlias(alias);
                    if (aliasPath != null)
                    {
                        WriteObject(aliasPath);
                    }
                    else
                    {
                        WriteObject("Alias not found");
                    }
                }
            }
        }
    }
}
