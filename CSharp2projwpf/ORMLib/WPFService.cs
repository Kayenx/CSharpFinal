using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLib
{
    public class WPFService
    {
        public static List<Book> GetBooks()
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            return or.Select_All<Book>();
        }
        public static Book GetBook(int id)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            return or.Select<Book>(id);
        }

        public static void InsertBook(Book book)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Insert<Book>(book);
        }
        public static void UpdateBook(Book book)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Update<Book>(book);
        }
        public static void DeleteBook(Book book)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Delete<Book>(book);
        }
        public static List<Reservation> GetReservations()
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            return or.Select_All<Reservation>();
        }
        public static Reservation GetReservation(int id)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            return or.Select<Reservation>(id);
        }

        public static void InsertReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Insert<Reservation>(reservation);
        }
        public static void UpdateReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Update<Reservation>(reservation);
        }
        public static void DeleteReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.connectionString = "Data Source=../../../../../database.db;";
            or.Delete<Reservation>(reservation);
        }
    }
}
