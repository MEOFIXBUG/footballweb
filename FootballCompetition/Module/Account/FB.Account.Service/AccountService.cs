

using FB.Account.Domain.Interface;
using FB.Account.Domain.Model;
using Microsoft.Practices.Unity;
using System;

namespace FB.Account.Service
{
    public class AccountService : IAccountService
    {
        [Dependency]
        public IAccountRepository AccountRepository { get; set; }
        public AccountModel AcceptLogin(LoginModel model)
        {
            try
            {
                return AccountRepository.AcceptLogin(model);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
