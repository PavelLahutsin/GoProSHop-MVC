using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace GoProShop.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) 
            : base(store)
        {
        }
    }
}
