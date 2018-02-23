using System;
using System.Collections.Generic;
using System.Text;

namespace Csb.Hiss.LdapManagement.Models
{
    public class LDAPUserData
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string Mail { get; internal set; }
    }
}
