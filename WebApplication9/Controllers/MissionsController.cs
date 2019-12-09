using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9;

namespace WebApplication9.Controllers
{
    [RequireHttps]
    public class MissionsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Missions
        public ActionResult Index()
        {
            return View(db.Missions.ToList());
        }        
    }
}
