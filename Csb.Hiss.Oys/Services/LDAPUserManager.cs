using Csb.Hiss.LdapManagement.Infrastructre;
using Csb.Hiss.LdapManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Csb.Hiss.Oys.Services
{
    public class LDAPUserManager : UserManager<IdentityUser>
    {
        public IUserAccessor LDAPUserAccessor { get; }
        private bool _isUserCreating = false;

        public LDAPUserManager(IUserAccessor userAccessor,
            IUserStore<IdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<IdentityUser>> logger
            )
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.LDAPUserAccessor = userAccessor;
        }

        public override async Task<IdentityUser> FindByNameAsync(string userName)
        {
            LDAPUserData userData = LDAPUserAccessor.GetUserData(userName);
            if (userData == null)
            {
                return null;
            }
            var dbUser = await base.FindByNameAsync(userName);
            if (dbUser != null)
            {
                return dbUser;
            }

            if (_isUserCreating)
                return null;

            dbUser = new IdentityUser(userName)
            {
                UserName = userName
            };
            _isUserCreating = true;
            await this.CreateAsync(dbUser);
            _isUserCreating = false;
            return dbUser;
        }

        public override async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {

            bool isValid = LDAPUserAccessor.IsUserCredentialsValid(user.UserName, password);
            if (isValid)
            {
                await this.RemovePasswordAsync(user);
                await this.AddPasswordAsync(user, password);
                return true;
            }

            return false;

        }
    }
}
