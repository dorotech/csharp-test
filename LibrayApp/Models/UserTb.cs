using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryApp.Models
{
    public partial class UserTb
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
