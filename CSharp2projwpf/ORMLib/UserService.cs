using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLib
{
   public class UserService
    {
        public static List<User> GetUsers()
        {
            ORM or = new ORM();
            return or.Select_All<User>();
        }
        public static User GetUser(int id)
        {
            ORM or = new ORM();
            return or.Select<User>(id);
        }

        public static void InsertUser(User user)
        {
            ORM or = new ORM();
            or.Insert<User>(user);
        }
        public static void UpdateUser(User user)
        {
            ORM or = new ORM();
            or.Update<User>(user);
        }
        public static void DeleteUser(User user)
        {
            ORM or = new ORM();
            or.Delete<User>(user);
        }
    }
}
