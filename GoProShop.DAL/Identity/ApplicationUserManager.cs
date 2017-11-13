﻿using GoProShop.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace GoProShop.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }   
    }
}
