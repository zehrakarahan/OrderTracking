using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ResponseModel
{
    public class LoginResponseModel
    {
        public string UserName {get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public bool LockedoutEnabled { get; set; }  
     
        public List<string> IdentityRoleList { get; set; }
    }
}
