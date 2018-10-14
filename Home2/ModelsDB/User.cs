using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2.ModelsDB
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}