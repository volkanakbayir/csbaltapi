using Csb.Hiss.LdapManagement.Infrastructre;
using Csb.Hiss.LdapManagement.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace Csb.Hiss.LdapManagement.Concrate
{
    public class UserAccessor : IUserAccessor
    {
        public LDAPUserData GetUserData(string username)
        {
            using (var context = ContextFactory.Create())
            {
                UserPrincipal userPrincipal = new UserPrincipal(context);
                userPrincipal.SamAccountName = username;
                PrincipalSearcher searcher = new PrincipalSearcher(userPrincipal);
                var principal = (UserPrincipal)searcher.FindOne();

                if (principal == null)
                    return null;

                LDAPUserData model = new LDAPUserData();
                model.DisplayName = principal.DisplayName;
                model.Name = principal.Name + " " + principal.MiddleName;
                model.Surname = principal.Surname;
                model.GivenName = principal.GivenName;
                model.Mail = principal.EmailAddress;
                return model;
            }
        }

        public bool IsUserCredentialsValid(string username, string password)
        {
            using (var context = ContextFactory.Create())
            {
                return context.ValidateCredentials(username, password);
            }
        }
    }
}
