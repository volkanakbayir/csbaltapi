using Csb.Hiss.LdapManagement.Constants;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace Csb.Hiss.LdapManagement.Concrate
{
    internal class ContextFactory
    {
        public static PrincipalContext Create()
        {
            PrincipalContext principalContext =
                new PrincipalContext(ContextType.Domain,
                LDAPConnectionConstants.LDAP_CONNECTION_ADDRESS,
                LDAPConnectionConstants.LDAP_CONNECTION_CLIENT_USERNAME,
                LDAPConnectionConstants.LDAP_CONNECTION_CLIENT_PASSWORD);
            return principalContext;
        }

        public static PrincipalContext Create(string subOUPath)
        {
            PrincipalContext principalContext =
            new PrincipalContext(ContextType.Domain,
            LDAPConnectionConstants.LDAP_CONNECTION_ADDRESS,
            subOUPath,
            LDAPConnectionConstants.LDAP_CONNECTION_CLIENT_USERNAME,
            LDAPConnectionConstants.LDAP_CONNECTION_CLIENT_PASSWORD);
            return principalContext;
        }
    }
}
