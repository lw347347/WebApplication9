using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication9;

namespace WebApplication9.Controllers
{
    [Authorize]
    public class MissionQuestionsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: MissionQuestions
        public ActionResult Index()
        {
            var missionQuestions = db.MissionQuestions.Include(m => m.Mission).Include(m => m.User);
            return View(missionQuestions.ToList());
        }

        // GET: MissionQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissionQuestion missionQuestion = db.MissionQuestions.Find(id);
            if (missionQuestion == null)
            {
                return HttpNotFound();
            }
            return View(missionQuestion);
        }

        // GET: MissionQuestions/Create
        public ActionResult Create()
        {
            ViewBag.MissionID = new SelectList(db.Missions, "MissionID", "MissionName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserEmail");
            return View();
        }

        // POST: MissionQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionQuestionID,MissionID,UserID,Question,Answer")] MissionQuestion missionQuestion)
        {
            if (ModelState.IsValid)
            {
                // Make the userID the same as the current user 
                // Find the user first
                string userToFind = User.Identity.Name;

                User originalUser = db.Users.FirstOrDefault(x => x.UserEmail == userToFind);

                missionQuestion.UserID = originalUser.UserID;

                db.MissionQuestions.Add(missionQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MissionID = new SelectList(db.Missions, "MissionID", "MissionName", missionQuestion.MissionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserEmail", missionQuestion.UserID);
            return View(missionQuestion);
        }

        // GET: MissionQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissionQuestion missionQuestion = db.MissionQuestions.Find(id);
            if (missionQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.MissionID = new SelectList(db.Missions, "MissionID", "MissionName", missionQuestion.MissionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserEmail", missionQuestion.UserID);
            return View(missionQuestion);
        }

        // POST: MissionQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionQuestionID,MissionID,UserID,Question,Answer")] MissionQuestion missionQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(missionQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MissionID = new SelectList(db.Missions, "MissionID", "MissionName", missionQuestion.MissionID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserEmail", missionQuestion.UserID);
            return View(missionQuestion);
        }        
    }
}
