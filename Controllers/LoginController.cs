using FallowUP.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace FallowUP.Controllers
{
    public class LoginController : Controller
    {
    public FollowUpEntities15 db = new FollowUpEntities15();
        //public string ad = "a@a";
        //public string pass = "a";

        [HttpPost]
        public ActionResult Index(USERS u,FormCollection fc) //u = formdan gelen bilgiler
        {
             
            var info = db.USERS.FirstOrDefault(m=> m.EMAIL==u.EMAIL && m.PASSWORD == u.PASSWORD);
            //var userType = db.USERS.Select(m => m.TYPE);
            //var userType = db.USERS.Where(m => m.EMAIL == u.EMAIL); //OBJEYİ GETİRİYOR
            //db.USERS.Where(m => m.EMAIL == u.EMAIL);
            //string mail = Request.QueryString["EMAIL"];
            

                if (info != null)
                {
                Session["Name"] = db.USERS.Where(x => x.EMAIL == u.EMAIL).FirstOrDefault().NAME + " " + db.USERS.Where(x => x.EMAIL == u.EMAIL).FirstOrDefault().SURNAME;
                Session["id"] = db.USERS.Where(x=> x.EMAIL==u.EMAIL).FirstOrDefault().ID;
                string gelenMail = u.EMAIL;
                USERS user = db.USERS.Where(x => x.EMAIL == gelenMail).FirstOrDefault();
                string userType = user.TYPE.ToString();
                if (userType=="1") //Admin
                {
                    Session["Yetki"] = "1";
                    return RedirectToAction("Index", "Admin");
                }
                if (userType == "2") //Ogretmen
                {
                    Session["Yetki"] = "2";
                    return RedirectToAction("Index", "Teacher");
                }
                if (userType == "3") //Ogrenci
                {
                    Session["Yetki"] = "3";
                    return RedirectToAction("Index", "Home");
                }
                }
                else
                {
                    ViewBag.Error = "Email veya şifre hatalı!";
                }
            return View();

        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}