# MyPathAliases

## Import in powershell
```
cd C:\projects\MyPathAliases\bin\Release\
Import-Module .\MyPathAliases.dll
```

## Use examples
```
Get-PathAlias "desktop"
Get-PathAlias -PrintAll

Get-PathAlias -AliasesJsonPath "C:\projects\MyPathAliases\path-aliases.json" -PrintAll
Get-PathAlias -AliasesJsonPath "C:\projects\MyPathAliases\path-aliases.json" -Alias "projects"
Get-PathAlias "projects" -AliasesJsonPath "C:\projects\MyPathAliases\path-aliases.json"

cd (Get-PathAlias -Alias "desktop" -AliasesJsonPath "C:\projects\MyPathAliases\path-aliases.json").path
```
