using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Models
{
    public partial class RentTb
    {
        public int IdRent { get; set; }
        public int IdBook { get; set; }
        public long Cpf { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }

        public virtual PersonTb CpfNavigation { get; set; }
        public virtual BookTb IdBookNavigation { get; set; }
    }
}
