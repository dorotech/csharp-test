using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Dto.Person
{
    public partial class PersonTbDto
    {
        public long Cpf { get; set; }
        public string Name { get; set; }
        public DateTime? Birth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
