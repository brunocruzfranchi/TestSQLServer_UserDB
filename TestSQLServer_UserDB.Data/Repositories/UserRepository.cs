using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSQLServer_UserDB.Model;

namespace TestSQLServer_UserDB.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public UserRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE
                        FROM user
                        WHERE [user_unique_id]=@Id";

            var result = await db.ExecuteAsync(sql, new { Id = user.user_unique_id });

            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT username, email, password, create_time, user_unique_id
                        FROM user ";

            return await db.QueryAsync<User>(sql, new { });
        }

        public List<User> GetAllUsersList()
        {
            var db = dbConnection();

            return db.Query<User>("Select * from user_db.[user]").ToList();
        }

        public async Task<User> GetUserDetails(string username)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT username, email, password, create_time, user_unique_id
                        FROM user 
                        WHERE username = @Username";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public bool InsertUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO [user_db].[user] ([username], [email], [password], [create_time])
                        VALUES (@username, @email, @password, @create_time)";

            var result = db.Execute(sql, new { username = user.username, email = user.email, password = user.password ,
            create_time = user.create_time});

            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();

            var sql = @"UPDATE [user_db].[user]
                       SET [username] = @username ,[email] = @email, [password] = @password
                       WHERE [user_unique_id] = @user_unique_id";

            var result = await db.ExecuteAsync(sql, new { user.username, user.email, user.password, user.user_unique_id});

            return result > 0;
        }
    }
}
