dnvm use 1.0.0-rc1-update1
dnx ef migrations add Initial
dnx ef database update


//Enable Migrations
Enable-Migrations

//Create migration after change/create one or more entities
Add-Migration ModificacaoXPTO

//Submit Migration on Database
Update-Database -Verbose

//Generate script
Update-Database -Script -SourceMigration:$InitialDataBase -TargetMigration:"ModificacaoXPTO"

//Reverting to "ModificacaoXPTO"
Update-Database -TargetMigration:"ModificacaoXPTO"