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
            var month = DateTime.Now.Month.ToString();

            var TotalClient = _db.Clients.Count().ToString();
            var SalesTaxClient = _db.Clients.Count(x => x.ClientTypeName.Equals("SalesTax")).ToString();
            var IncomeTaxClient = _db.Clients.Count(x => x.ClientTypeName.Equals("IncomeTax")).ToString();

            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var monthlyclient = (from m in _db.Clients
                                     where m.RegistrationDate> dtFrom && m.RegistrationDate<dtTo 
                                     select m).Count().ToString();

            ViewBag.lstTotalClient = TotalClient;
            ViewBag.lstSalesTaxClient = SalesTaxClient;
            ViewBag.lstIncomeTaxClient = IncomeTaxClient;
            ViewBag.lstThisMonthClient = monthlyclient;


            return View();
        }
        public ActionResult UserList()
        {
            var lstUsers = (from u in _db.UserAccounts
                            select u).AsEnumerable();
            return View(lstUsers);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserAccounts model)
        {
            _db.UserAccounts.Add(model);
            _db.SaveChanges();
            return RedirectToAction("UserList");
        }
        public ActionResult EditUser(int id)
        {
            var editUser = _db.UserAccounts.Where(x => x.UserId.Equals(id)).FirstOrDefault();

            return View(editUser);
        }
        [HttpPost]
        public ActionResult EditUser(UserAccounts model)
        {
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("UserList");
        }
        
        public ActionResult DeleteUser(int id)
        {
            UserAccounts model = _db.UserAccounts.Find(id);
            _db.UserAccounts.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("UserList");
        }
        public ActionResult AddDocument()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDocument(string DocumentName, HttpPostedFileBase file)
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
        public ActionResult DocumentList()
        {
            var lstDoc = _db.Document.ToList();
            return View(lstDoc);
        }
        public ActionResult AddClient()
        {
            ViewBag.ClientType = new SelectList(_db.ClientTypes, "ClienttypeId", "ClientTypeName");
            ViewBag.City = new SelectList(_db.Cities, "CityId", "CityName");
            ViewBag.State = new SelectList(_db.States, "StateId", "StateName");
            ViewBag.Return = new SelectList(_db.ReturnTypes, "ReturnTypeId", "ReturnTypeName");
            return View();
        }
        //string ClientName, string Proprietor, int CNICNumber, string Address, DateTime RegistrationDate, string Representative, int City, int State, int Return, int ClientType, bool Services, bool Importer, bool Exporter, bool WholeSeller, bool Retailer, bool CommercialImporter, int SalesTaxNumber, int NTNNumber, int MobileNumber1, int MobileNumber2, int OfficeNumber1, int OfficeNumber2, string EmailAddress, int PIN, string UserId, string Password
        [HttpPost]
        public ActionResult SaveClient(Client model)
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
            try
            {
                _db.Clients.Add(model);
                _db.SaveChanges();
               // status = true;
                return RedirectToAction("AddClient");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
        }
        public ActionResult SalesTaxList()
        {
            var STList = _db.Clients.Where(x => x.ClientTypeId.Equals(1)).ToList();
            return View(STList);
        }
        public ActionResult IncomeTaxList()
        {
            var ITList = _db.Clients.Where(x => x.ClientTypeId.Equals(2)).ToList();
            return View(ITList);
        }
        public ActionResult GeneralList()
        {
            var lstgeneral = _db.Clients.ToList();
            return View(lstgeneral);
        }
        public ActionResult EditClient(int id)
        {
            ViewBag.ClientType = new SelectList(_db.ClientTypes, "ClienttypeId", "ClientTypeName");
            ViewBag.City = new SelectList(_db.Cities, "CityId", "CityName");
            ViewBag.State = new SelectList(_db.States, "StateId", "StateName");
            ViewBag.Return = new SelectList(_db.ReturnTypes, "ReturnTypeId", "ReturnTypeName");
           
            var findlst = _db.Clients.Find(id);
            return View(findlst);
        }
        [HttpPost]
        public ActionResult EditClient(Client model)
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
            try
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            catch
            {
                return RedirectToAction("~/Views/Shared.Error.CSHTML");
            }
            
        }
        public ActionResult DetailsClient(int id)
        {
            var Client = _db.Clients.Find(id);
            return View(Client);
        }
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmailSucces(string EmailAddress, string text, string Subject)
        {
            string EmailFromAddress = WebConfigurationManager.AppSettings["EmailFromAddress"];

            if (EmailAddress != null)
            {
                string body = "<div style='text-align:center;'>" +text + "</div>";
               
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
    }
}