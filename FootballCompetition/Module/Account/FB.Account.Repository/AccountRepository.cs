using Dapper;
using FB.Account.Domain.Interface;
using FB.Account.Domain.Model;
using Infras.Common;
using System.Linq;

namespace FB.Account.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public AccountModel AcceptLogin(LoginModel model)
        {
            var param = new DynamicParameters();
            param.Add("userName", model.userName);
            param.Add("passWord", model.passWord);
            return DALHelpers.QueryByStored<AccountModel>("sp_AcceptLogin", param).FirstOrDefault();
        }
    }
}
