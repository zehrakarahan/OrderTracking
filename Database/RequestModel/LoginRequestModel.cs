using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.RequestModel
{
    public class LoginRequestModel
    {
        public string UserNameOrEmail { get; set; }

        public string Password { get; set; }
    }
}
