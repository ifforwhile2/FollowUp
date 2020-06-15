using FallowUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.Ajax.Utilities;

namespace FallowUP.Controllers
{
    [AuthorityAdmin]
    public class AdminController : Controller
    {
        // public FollowUpEntities2 db = new FollowUpEntities2();
        public FollowUpEntities15 db = new FollowUpEntities15();
        public ActionResult Index()
        {
            string name = Session["Name"].ToString();
            ViewData["name"] = name; //Sağ üstteki ad kısmı için layouta yolluyoruz
            ViewData["countTeachers"] = db.USERS.Where(x => x.TYPE == 2 && x.ACTIVE == true).ToList().Count;
            ViewData["countStudents"] = db.USERS.Where(x => x.TYPE == 3 && x.ACTIVE == true).ToList().Count;
            ViewData["countClasses"] = db.TEACHERCLASS.Where(x => x.ACTIVE == true).ToList().Count;
            ViewData["countHomeworks"] = db.HOMEWORK.Where(x => x.ACTIVE == true).ToList().Count;
            ViewData["countNotes"] = db.STUDENTHOMEWORK.Where(x => x.ACTIVE == true).ToList().Count;

            //if (Session["Yetki"]=="1")
            //{
            //return View();

            //}
            //else
            //{
            //    Session.Remove("Yetki");
            //    return Redirect("/Login/Index");
            //}
            return View();
        }
        public ActionResult TeachersList(int page=1)
        {
            
            //var values = db.USERS.ToList();
            var values = db.USERS.Where(x=>x.TYPE==2 && x.ACTIVE==true).ToList().ToPagedList(page, 5);
            return View(values);
        }
        
        public ActionResult StudentsList(int page = 1)
        {
            var values = db.USERS.Where(x => x.TYPE == 3 && x.ACTIVE==true).ToList().ToPagedList(page, 5);
            return View(values);
        }
        public ActionResult ClassesList(int page = 1)
        {
            var values = db.TEACHERCLASS.Where(x=>x.ACTIVE==true).ToList().ToPagedList(page, 5); //hata alırsan whereyi sil
            return View(values);
        }
        public ActionResult HomeworkList(int page = 1)
        {
            var values = db.HOMEWORK.Where(x=>x.ACTIVE==true).ToList().ToPagedList(page, 5); //hata alırsan whereyi sil
            return View(values);
        }
        //USER
        public ActionResult UpdateUser(USERS u)
        {
            var user = db.USERS.Find(u.ID);
            user.NAME = u.NAME;
            user.SURNAME = u.SURNAME;
            user.EMAIL = u.EMAIL;
            user.PASSWORD = u.PASSWORD;
            user.TYPE = u.TYPE;
            db.SaveChanges();

            return Redirect("/Admin/Index");
        }
        public ActionResult GetUser(int id)
        {
            var user = db.USERS.Find(id);
            return View("GetUser",user);
        }
        [HttpGet]
        public ActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUser(USERS u)
        {
            u.ACTIVE = true; //? ++calisiyor
            bool a = u.ACTIVE;
            db.USERS.Add(u);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteUser(int id)
        {
            var user = db.USERS.Find(id);
            user.ACTIVE = false;
            //db.USERS.Remove(user);
            db.SaveChanges();
            return Redirect("/Admin/Index");
        }
        //CLASS
        public ActionResult UpdateClass(TEACHERCLASS t)
        {
            var clas = db.TEACHERCLASS.Find(t.CLASSID);
            clas.NAME = t.NAME;
            clas.PASSWORD = t.PASSWORD;
            clas.TEACHERID = t.TEACHERID;
            clas.TEACHERNAME = t.TEACHERNAME;
            clas.TEACHERSURNAME = t.TEACHERSURNAME;
            db.SaveChanges();

            return Redirect("/Admin/ClassesList");
        }
        public ActionResult GetClass(int id)
        {
            var clas = db.TEACHERCLASS.Find(id);
            return View("GetClass", clas);
        }
        [HttpGet]
        public ActionResult AddNewClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewClass(TEACHERCLASS t)
        {
            t.ACTIVE = true;
            t.TEACHERNAME = db.USERS.Find(t.TEACHERID).NAME;
            t.TEACHERSURNAME = db.USERS.Find(t.TEACHERID).SURNAME;
            db.TEACHERCLASS.Add(t);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteClass(int id)
        {
            var clas = db.TEACHERCLASS.Find(id);
            clas.ACTIVE = false;
            db.SaveChanges();
            return Redirect("/Admin/ClassesList");
        }
        //HOMEWORK
        public ActionResult GetHomework(int id)
        {
            var homework = db.HOMEWORK.Find(id);
            return View("GetHomework", homework);
        }
        public ActionResult UpdateHomework(HOMEWORK h)
        {
            var homework = db.HOMEWORK.Find(h.HOMEWORKID);
            homework.CLASSID = h.CLASSID;
            homework.TEACHERID = h.TEACHERID;
            homework.HOMEWORKNAME = h.HOMEWORKNAME;
            homework.HOMEWORK1 = h.HOMEWORK1;
            homework.STARTDATE = h.STARTDATE;
            homework.FINISHDATE = h.FINISHDATE;
            db.SaveChanges();

            return Redirect("/Admin/HomeworkList");
        }
        public ActionResult AddNewHomework()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewHomework(HOMEWORK h)
        {
            h.ACTIVE = true;
            db.HOMEWORK.Add(h);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteHomework(int id)
        {
            var clas = db.HOMEWORK.Find(id);
            clas.ACTIVE = false;
            db.SaveChanges();
            return Redirect("/Admin/HomeworkList");
        }
        public ActionResult NotesList(int page = 1)
        {
            var values = db.STUDENTHOMEWORK.Where(x =>  x.ACTIVE == true).ToList().ToPagedList(page, 5);
            return View(values);
        }
        public ActionResult GetNote(int id)
        {
            var note = db.STUDENTHOMEWORK.Find(id);
            return View("GetNote", note);
        }
        [HttpGet]
        public ActionResult AddNewNote()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewNote(STUDENTHOMEWORK n)
        {
            n.ACTIVE = true; //? ++calisiyor
            db.STUDENTHOMEWORK.Add(n);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteNote(int id)
        {
            var note = db.STUDENTHOMEWORK.Find(id);
            note.ACTIVE = false;
            db.SaveChanges();
            return Redirect("/Admin/NotesList");
        }
        public ActionResult UpdateNote(STUDENTHOMEWORK n)
        {
            var note = db.STUDENTHOMEWORK.Find(n.ID);
            note.ACTIVE = true;
            note.STUDENTID = n.STUDENTID;
            note.TEACHERID = n.TEACHERID;
            note.CLASSID = n.CLASSID;
            note.NOTE = n.NOTE;
            note.STUDENTNAME = n.STUDENTNAME;
            note.STUDENTSURNAME = n.STUDENTSURNAME;
            note.TEACHERNAME = n.TEACHERNAME;
            note.TEACHERSURNAME = n.TEACHERSURNAME;
            db.SaveChanges();

            return Redirect("/Admin/NotesList");
        }
        public ActionResult MessagesList(int page = 1)
        {
            int id = int.Parse(Session["id"].ToString());
            var values = db.MESSAGES.Where(x => x.TOID == id&&x.ACTIVE == true).ToList().ToPagedList(page, 5); //bana geldiyse al listele
            return View(values);
        }
        public ActionResult GetMessage(int id)
        {
            var msj = db.MESSAGES.Find(id);
            ViewData["frommail"] =msj.FROMMAIL;
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
            return Redirect("/Admin/MessagesList");
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
    }
}