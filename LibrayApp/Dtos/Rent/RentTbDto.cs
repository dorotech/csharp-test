using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Dto.Rent
{
    public partial class RentTbDto
    {
        public int IdBook { get; set; }
        public long Cpf { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
