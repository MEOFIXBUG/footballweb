using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Account.Domain.Model
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }
    public class AccountModel
    {
        public int id { get; set; }
        public int role { get; set; }
        public string linkAvatar { get; set; }
        public string fullName { get; set; }
        public string genders { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime birthday { get; set; }
        public string address { get; set; }
        public int status { get; set; }


    }
    public class Roles
    {
        public int id { get; set; }
        public string byName { get; set; }
    }
}
