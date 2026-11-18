using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.DAL.Models
{
    public class TimeSlot
    {
        public int SlotId { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}
