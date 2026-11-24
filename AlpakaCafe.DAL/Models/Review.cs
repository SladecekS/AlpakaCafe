using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AlpakaCafe.DAL.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
