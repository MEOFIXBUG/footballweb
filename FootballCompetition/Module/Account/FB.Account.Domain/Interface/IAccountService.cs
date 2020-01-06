using FB.Account.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Account.Domain.Interface
{
    public interface IAccountService
    {
        LoginModel AcceptLogin(LoginModel model);
    }
}
