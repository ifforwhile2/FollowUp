using FallowUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FallowUP.Controllers
{
    [AuthorityTeacher]
    public class TeacherController : Controller
    {
        public FollowUpEntities15 db = new FollowUpEntities15();
        public ActionResult Index()
        {
            string name = Session["Name"].ToString();
            ViewData["name"] = name; //Sağ üstteki ad kısmı için layouta yolluyoruz
            //if (Session["Yetki"] == "2")
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
        
        public ActionResult MyClassesList(int page = 1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.TEACHERCLASS.Where(x => x.TEACHERID==id &&x.ACTIVE == true).ToList().ToPagedList(page, 5); //hata alırsan whereyi sil
            return View(values);
        }
        public ActionResult GetMyClass(int id) //düzelt
        {
            var clas = db.TEACHERCLASS.Find(id);
            return View("GetMyClass", clas);
        }
    }
}