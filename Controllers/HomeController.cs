using FallowUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.Ajax.Utilities;
using System.Web.UI;

namespace FallowUP.Controllers
{   
    [AuthorityStudent]
    public class HomeController : Controller
    {
        public FollowUpEntities15 db = new FollowUpEntities15();
        public ActionResult Index()
        {
            string name = Session["Name"].ToString();
            ViewData["name"] = name; //Sağ üstteki ad kısmı için layouta yolluyoruz

            //if (Session["Yetki"] == "3")
            //{
            //    return View();

            //}
            //else
            //{
            //    Session.Remove("Yetki");
            //    return Redirect("/Login/Index");
            //}
            return View();
        }
        public ActionResult MyClassList(int page=1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.STUDENTCLASS.Where(x => x.STUDENTID == id && x.ACTIVE == true).ToList().ToPagedList(page, 8);
            return View(values);
        }
        [HttpGet]
        public ActionResult AddNewClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewClass(STUDENTCLASS s)
        {
            string pass = db.TEACHERCLASS.Where(x => x.CLASSID == s.CLASSID).FirstOrDefault().PASSWORD;
            int id = int.Parse(Session["id"].ToString());
            s.STUDENTID = id;
            s.CLASSNAME= db.TEACHERCLASS.Find(s.CLASSID).NAME;
            s.ACTIVE = true;
            if (s.CLASSPASSWORD==pass)
            {
                db.STUDENTCLASS.Add(s);
                db.SaveChanges();
                return View();
            }
            ViewData["Error"] ="ID veya Şifre Hatalı !";
            return View();
            
        }
        public ActionResult MyNoteList(int page = 1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.STUDENTHOMEWORK.Where(x => x.STUDENTID == id && x.ACTIVE == true).ToList().ToPagedList(page, 8);
            return View(values);
        }

        public ActionResult MyHomeworkList(int page = 1) //calısmıyor
        {
            int id = int.Parse(Session["id"].ToString());
            // var values = db.HOMEWORK.Where(x => x. == id && x.ACTIVE == true).ToList().ToPagedList(page, 8);
            // return View(values);
            return View();
        }
        ////
        public ActionResult MessagesList(int page = 1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.MESSAGES.Where(x => x.TOID == id && x.ACTIVE == true).ToList().ToPagedList(page, 5); //bana geldiyse al listele
            return View(values);
        }
        public ActionResult GetMessage(int id)
        {
            var msj = db.MESSAGES.Find(id);
            ViewData["frommail"] = msj.FROMMAIL;
            ViewData["fromname"] = msj.FROMNAME;
            ViewData["fromsurname"] = msj.FROMSURNAME;
            ViewData["frommail"] = msj.FROMMAIL;
            ViewData["date"] = msj.DATE;
            ViewData["title"] = msj.TITLE;
            ViewData["frommail"] = msj.FROMMAIL;
            ViewData["msj"] = msj.MESSAGE;
            return View("GetMessage");
        }
        public ActionResult DeleteMessage(int id)
        {
            var msj = db.MESSAGES.Find(id);
            msj.ACTIVE = false;
            db.SaveChanges();
            return Redirect("/Home/MessagesList");
        }
        public ActionResult AddNewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewMessage(MESSAGES m)
        {
            int id = int.Parse(Session["id"].ToString());
            m.ACTIVE = true; //? ++calisiyor
            m.FROMID = id;
            bool a = m.ACTIVE; //test için
            db.MESSAGES.Add(m);
            db.SaveChanges();
            return View();
        }
        public ActionResult HomeworkList(int page = 1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.STUDENTHOMEWORK.Where(x => x.STUDENTID == id && x.ACTIVE == true).ToList().ToPagedList(page, 8);
            return View(values);
        }
        public ActionResult GetHomework(int id)
        {
            var hm = db.STUDENTHOMEWORK.Find(id);
            ViewData["Hname"] = hm.HOMEWORKNAME;
            ViewData["Hh"] = hm.HOMEWORK;
            ViewData["He"] = hm.ENDDATE;
            ViewData["Hcn"] = hm.CLASSNAME;
            return View("GetHomework");
        }
        
    }
}