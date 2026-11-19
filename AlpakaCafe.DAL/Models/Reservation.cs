using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AlpakaCafe.DAL.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime ReservationDate { get; set; } // ovde je bilo kolona CreatedAt ako mi ne bude radila migracija mozda je to toga

        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }


    }
}
