using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLServer_UserDB.Model
{
    public class User
    {

        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime create_time { get; set; }
        public int user_unique_id { get; set; }

    }
}
