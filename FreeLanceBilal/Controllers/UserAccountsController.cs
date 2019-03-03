using FreeLanceBilal.Classes;
using FreeLanceBilal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FreeLanceBilal.Controllers
{
    public class UserAccountsController : Controller
    {
        // GET: Some commen methods
        private readonly MyDbContext _db;
        public UserAccountsController()
        {
            _db = new MyDbContext();
        }
        PasswordHelper ph = new PasswordHelper();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginSuccess(string UserName , string Password,bool RememberMe)
        {
            if (RememberMe)
            {
                HttpCookie cookie = new HttpCookie("RememberMe");
                cookie.Values.Add("UserNameRemember", UserName);
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }
            else
            {
                if (Request.Cookies.AllKeys.Contains("RememberMe"))
                {
                    HttpCookie cookie = Request.Cookies["RememberMe"];
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            var success = _db.UserAccounts.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password)).FirstOrDefault();

            if (success != null)
            {
                Session["UserId"] = success.UserId;
                Session["UserName"] = success.UserName;

                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                Session.Remove("UserId");
                Session.Remove("UserName");

                return RedirectToAction("Login", "UserAccounts");
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");

            return RedirectToAction("Login");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ForgotPasswordSuccess(string Email)
        {
            var success = (from e in _db.UserAccounts
                           where e.EmailAddress.Equals(Email)
                           select e).FirstOrDefault();
            string EmailFromAddress = WebConfigurationManager.AppSettings["EmailFromAddress"];

            if (success!=null)
            {
                string body = "<div style='text-align:center;'>"+@DateTime.Now.Date.ToShortDateString()+"</div>";
                body += "<div style=text-align:center;'><h1>Forgot your Password</h1></div>";
                body += "<table>";
                body += "<tr><td>User Name : </td><td>" + success.UserName + "</td></tr>";
                body += "<tr><td>Password : </td><td>" + success.Password + "</td></tr>";
                body += "</table>";

                var MailHelper = new MailHelper()
                {

                    Sender = EmailFromAddress,
                    Recipient = Email,
                    RecipientCC = null,
                    Subject = "Did You Forgot Your Password? | BILAL CORPORATE SYSTEM",
                    Body = body
                };
                MailHelper.Send();


                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("ForgotPassword", new { IsError = true });
            }
        }


    }
}