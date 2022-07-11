using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLServer_UserDB.Data.Repositories
{
    public interface IDatabaseRepository
    {

        bool CreateDatabase(string databaseName);

        bool CreateTables(string databaseName);

        bool CheckDatabaseExists(string databaseName);

        bool GrantAccess(string fullPath);
        
    }
}
