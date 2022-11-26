using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Dto.Book
{
    public partial class BookTbDto
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public DateTime? WrittenDate { get; set; }
        public string BarCode { get; set; }
        public int AvailableQuantity { get; set; }
        public int RentedQuantity { get; set; }
    }
}
