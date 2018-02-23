using Csb.Hiss.LdapManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csb.Hiss.LdapManagement.Infrastructre
{
    public interface IUserAccessor : IUserValidator
    {
        LDAPUserData GetUserData(string username); 
    }
}
