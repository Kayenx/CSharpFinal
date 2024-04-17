using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLib
{
    public class Reservation
    {
        public int ID { get; set; }
        public int User_Id { get; set; }
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
