using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonGlass_2019.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<int> CategoryList { get; set; }
    }
}