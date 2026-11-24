using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AlpakaCafe.DAL.Models
{
    public class Alpaca
    {
        public int AlpacaId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime BirthDate { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
