using System;
using System.Collections.Generic;
using System.Text;

namespace Csb.Hiss.LdapManagement.Infrastructre
{
    public interface IUserValidator
    {
        bool IsUserCredentialsValid(string username, string password);
    }
}
