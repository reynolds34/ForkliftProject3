using System;
using System.Collections.Generic;

namespace ForkliftProject3.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserAccountId { get; set; }
    }
}
