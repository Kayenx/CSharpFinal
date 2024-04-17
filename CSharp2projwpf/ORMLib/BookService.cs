using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLib
{
    public class BookService
    {
        public static List<Book> GetBooks()
        {
            ORM or = new ORM();
            return or.Select_All<Book>();
        }
        public static Book GetBook(int id)
        {
            ORM or = new ORM();
            return or.Select<Book>(id);
        }

        public static void InsertBook(Book book)
        {
            ORM or = new ORM();
            or.Insert<Book>(book);
        }
        public static void UpdateBook(Book book)
        {
            ORM or = new ORM();
            or.Update<Book>(book);
        }
        public static void DeleteBook(Book book)
        {
            ORM or = new ORM();
            or.Delete<Book>(book);
        }
    }
}
