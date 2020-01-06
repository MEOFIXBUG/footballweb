using FB.Account.Domain.Interface;
using FB.Account.Domain.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballCompetition.Controllers.Account
{
    public class AccountController : Controller
    {
        // GET: Account
        [Dependency]
        public IAccountService AccountService { get; set; }
        public ActionResult Login()
        {
            var model = new LoginModel();
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var res = AccountService.AcceptLogin(model);
            return RedirectToAction("Login");
        }
        public ActionResult ABC()
        {
           
            return View();
        }

    }
}