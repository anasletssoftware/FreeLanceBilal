using FreeLanceBilal.Classes;
using FreeLanceBilal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FreeLanceBilal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private readonly MyDbContext _db;
        public AdminController()
        {
            _db = new MyDbContext();
        }
        PasswordHelper ph = new PasswordHelper();



        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var TotalClient = _db.Clients.Count().ToString();
                var SalesTaxClient = _db.Clients.Count(x => x.ClientTypeName.Equals("SalesTax")).ToString();
                var IncomeTaxClient = _db.Clients.Count(x => x.ClientTypeName.Equals("IncomeTax")).ToString();

                DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var monthlyclient = (from m in _db.Clients
                                     where m.RegistrationDate > dtFrom && m.RegistrationDate < dtTo
                                     select m).Count().ToString();

                ViewBag.lstTotalClient = TotalClient;
                ViewBag.lstSalesTaxClient = SalesTaxClient;
                ViewBag.lstIncomeTaxClient = IncomeTaxClient;
                ViewBag.lstThisMonthClient = monthlyclient;


                return View();
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult UserList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var lstUsers = (from u in _db.UserAccounts
                                select u).AsEnumerable();
                return View(lstUsers);
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult AddUser()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserAccounts model)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Login", "UserAccounts");
                }
                _db.UserAccounts.Add(model);
                _db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult EditUser(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var editUser = _db.UserAccounts.Where(x => x.UserId.Equals(id)).FirstOrDefault();

                return View(editUser);
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        [HttpPost]
        public ActionResult EditUser(UserAccounts model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }

        public ActionResult DeleteUser(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                UserAccounts model = _db.UserAccounts.Find(id);
                _db.UserAccounts.Remove(model);
                _db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult AddDocument()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SaveDocument(string DocumentName, HttpPostedFileBase file)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                Documents model = new Documents();

                string FilePath = WebConfigurationManager.AppSettings["UserUploadsPath"];
                string UserDocument = "";

                if (file.ContentLength > 0)
                {
                    UserDocument = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath(FilePath), UserDocument);
                    // file is uploaded
                    file.SaveAs(path);
                }
                model.DocumentName = DocumentName;
                model.Document = FilePath + UserDocument;

                _db.Document.Add(model);
                _db.SaveChanges();


                return RedirectToAction("DocumentList");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult DocumentList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            var lstDoc = _db.Document.ToList();
            return View(lstDoc);
        }
        public ActionResult AddClient()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            ViewBag.ClientType = new SelectList(_db.ClientTypes, "ClienttypeId", "ClientTypeName");
            ViewBag.City = new SelectList(_db.Cities, "CityId", "CityName");
            ViewBag.State = new SelectList(_db.States, "StateId", "StateName");
            ViewBag.Return = new SelectList(_db.ReturnTypes, "ReturnTypeId", "ReturnTypeName");
            return View();
        }
        [HttpPost]
        public ActionResult SaveClient(Client model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var CityName = (from c in _db.Cities
                                where c.CityId.Equals(model.CityId)
                                select c.CityName).FirstOrDefault();
                var StateName = (from s in _db.States
                                 where s.StateId.Equals(model.StateId)
                                 select s.StateName).FirstOrDefault();
                var ReturnType = (from r in _db.ReturnTypes
                                  where r.ReturnTypeId.Equals(model.ReturntypeId)
                                  select r.ReturnTypeName).FirstOrDefault();
                var ClientType = (from c in _db.ClientTypes
                                  where c.ClienttypeId.Equals(model.ClientTypeId)
                                  select c.ClientTypeName).FirstOrDefault();
                model.CityName = CityName;
                model.StateName = StateName;
                model.ReturnTypeName = ReturnType;
                model.ClientTypeName = ClientType;


                _db.Clients.Add(model);
                _db.SaveChanges();
                // status = true;
                return RedirectToAction("AddClient");
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult SalesTaxList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var STList = _db.Clients.Where(x => x.ClientTypeId.Equals(1)).ToList();
                return View(STList);
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult IncomeTaxList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            var ITList = _db.Clients.Where(x => x.ClientTypeId.Equals(2)).ToList();
            return View(ITList);
        }
        public ActionResult GeneralList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var lstgeneral = _db.Clients.ToList();
                return View(lstgeneral);
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult EditClient(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                ViewBag.ClientType = new SelectList(_db.ClientTypes, "ClienttypeId", "ClientTypeName");
                ViewBag.City = new SelectList(_db.Cities, "CityId", "CityName");
                ViewBag.State = new SelectList(_db.States, "StateId", "StateName");
                ViewBag.Return = new SelectList(_db.ReturnTypes, "ReturnTypeId", "ReturnTypeName");

                var findlst = _db.Clients.Find(id);
                return View(findlst);
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
        [HttpPost]
        public ActionResult EditClient(Client model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var CityName = (from c in _db.Cities
                                where c.CityId.Equals(model.CityId)
                                select c.CityName).FirstOrDefault();
                var StateName = (from s in _db.States
                                 where s.StateId.Equals(model.StateId)
                                 select s.StateName).FirstOrDefault();
                var ReturnType = (from r in _db.ReturnTypes
                                  where r.ReturnTypeId.Equals(model.ReturntypeId)
                                  select r.ReturnTypeName).FirstOrDefault();
                var ClientType = (from c in _db.ClientTypes
                                  where c.ClienttypeId.Equals(model.ClientTypeId)
                                  select c.ClientTypeName).FirstOrDefault();
                model.CityName = CityName;
                model.StateName = StateName;
                model.ReturnTypeName = ReturnType;
                model.ClientTypeName = ClientType;

                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }

        }
        public ActionResult DetailsClient(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                var Client = _db.Clients.Find(id);
                return View(Client);
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult SendEmail()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmailSucces(string EmailAddress, string text, string Subject)
        {
            //            After sign into google account, go to:

            //myaccount.google.com/lesssecureapps 
            //or 
            //www.google.com/settings/security/lesssecureapps 
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "UserAccounts");
            }
            try
            {
                string EmailFromAddress = WebConfigurationManager.AppSettings["EmailFromAddress"];

                if (EmailAddress != null)
                {
                    string body = "<div style='text-align:center;'>" + text + "</div>";

                    body += "</table>";

                    var MailHelper = new MailHelper()
                    {

                        Sender = EmailFromAddress,
                        Recipient = EmailAddress,
                        RecipientCC = null,
                        Subject = Subject,
                        Body = body
                    };
                    MailHelper.Send();


                    return RedirectToAction("SendEmail");
                }
                else
                {
                    return RedirectToAction("SendEmail", new { IsError = true });
                }
            }
            catch
            {
                return Redirect("~/Views/Shared.Error.CSHTML");
            }
        }
    }
}