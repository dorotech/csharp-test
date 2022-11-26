using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Models
{
    public partial class PersonTb
    {
        public PersonTb()
        {
            RentTbs = new HashSet<RentTb>();
        }

        public long Cpf { get; set; }
        public string Name { get; set; }
        public DateTime? Birth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<RentTb> RentTbs { get; set; }
    }
}
