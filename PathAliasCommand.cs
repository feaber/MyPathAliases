using System.Management.Automation;
using System.IO;


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
        private string jsonPath = "path-aliases.json";

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
        }

        protected override void ProcessRecord()
        {
            try
            {
                pathAliasManager.ParseJsonFile(jsonPath);
            } catch(FileNotFoundException e)
            {
                WriteObject(e.Message);
                return;
            }

            if (string.IsNullOrEmpty(alias) && !printAll)
            {
                WriteObject("You must provide at least one of parapeters: Alias, PrintAll");
                return;
            } 
            else {
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
