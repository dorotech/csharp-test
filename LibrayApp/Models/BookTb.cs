using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Models
{
    public partial class BookTb
    {
        public BookTb()
        {
            RentTbs = new HashSet<RentTb>();
        }

        public int IdBook { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public DateTime? WrittenDate { get; set; }
        public string BarCode { get; set; }
        public int AvailableQuantity { get; set; }
        public int RentedQuantity { get; set; }

        public virtual ICollection<RentTb> RentTbs { get; set; }
    }
}
