﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ResponseModel
{
    public class WebToken
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string UserId { get; set; }
    }
}
