using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLib
{
    public class ReservationService
    {
        public static List<Reservation> GetReservations()
        {
            ORM or = new ORM();
            return or.Select_All<Reservation>();
        }
        public static Reservation GetReservation(int id)
        {
            ORM or = new ORM();
            return or.Select<Reservation>(id);
        }

        public static void InsertReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.Insert<Reservation>(reservation);
        }
        public static void UpdateReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.Update<Reservation>(reservation);
        }
        public static void DeleteReservation(Reservation reservation)
        {
            ORM or = new ORM();
            or.Delete<Reservation>(reservation);
        }
    }
}
