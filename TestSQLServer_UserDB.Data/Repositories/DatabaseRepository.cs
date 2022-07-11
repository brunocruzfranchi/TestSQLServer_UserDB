using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSQLServer_UserDB.Model;
using Dapper;
using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;


namespace TestSQLServer_UserDB.Data.Repositories
{

    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public DatabaseRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public bool CreateDatabase(string databaseName)
        {
            // ToDo: Agregar control de excepciones 

            var db = dbConnection();

            //string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            bool doesExist = CheckDatabaseExists(databaseName);

            if (!doesExist) {
                var sql = SQL.CreateUserDB;

                var sqlqueries = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries); // Separamos en diferentes queries ya que el OEM no es capaz de leer los GO

                foreach (var query in sqlqueries) { db.Execute(query); }

                return true;
            }

            return false;
            
        }

        public bool CreateTables(string databaseName)
        {
            // ToDo: Agregar control de excepciones 

            var db = dbConnection();

            bool doesExist = CheckDatabaseExists(databaseName);

            if (doesExist)
            {
                var sql = SQL.CreateUserDBFull;

                var sqlqueries = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries); // Separamos en diferentes queries ya que el OEM no es capaz de leer los GO

                foreach (var query in sqlqueries) { db.Execute(query); }

                return true;
            }

            return false;

        }

        public bool CheckDatabaseExists(string databaseName)
        {
            var db = dbConnection();
           
            var sql = @"
                    SELECT name, database_id
                    FROM sys.databases
                    WHERE name = @DatabaseName";
                
            var result = db.Query<Database>(sql, new { DatabaseName = databaseName});

            return result.Count() > 0;
        }

        // No sirve
        public bool GrantAccess(string fullPath) 
        {
            DirectoryInfo info = new DirectoryInfo(fullPath);
            WindowsIdentity self = System.Security.Principal.WindowsIdentity.GetCurrent();
            DirectorySecurity ds = info.GetAccessControl();
            
            ds.AddAccessRule(new FileSystemAccessRule(self.Name,
                FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit |
                InheritanceFlags.ContainerInherit,
                PropagationFlags.None,
                AccessControlType.Allow));

            info.SetAccessControl(ds);

            return true;
        }

    }
}
