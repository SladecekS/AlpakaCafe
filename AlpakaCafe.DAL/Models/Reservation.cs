using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.DAL.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}
