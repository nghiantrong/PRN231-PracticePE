using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class UserAccount
    {
        public int UserAccountId { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public int? Role { get; set; }
    }
}
