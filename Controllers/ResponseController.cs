using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Practice.Controllers
{
    public class ResponseController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Response
        public ActionResult Index()
        {
            ViewBag.projects = new SelectList(db.Project, "ProjectId", "Name");
            return View(db.Response.Include(r => r.Project).Include(r => r.User).ToList());
        }

        // GET: Response/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Response.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", response);
        }

        // GET: Response/Create
        public ActionResult Create(int? id)
        {
            List<string> projects = new List<string> ();

            foreach (var row in db.Project)
            {
                projects.Add(row.Name);
            }

            ViewBag.Projects = projects;
            ViewBag.Selected = (db.Project.Find(id).Name != null) ? db.Project.Find(id).Name : null;

            return View();
        }
        
        // POST: Response/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,PhoneNumber")] Response response, string Name)
        {
            var project = db.Project.Where(r => r.Name == Name);
            System.Diagnostics.Debug.WriteLine(Name);
            if (ModelState.IsValid && project.Count() == 1)
            {
                response.ProjectId = project.First().ProjectId;
                response.Checked = false;
                db.Response.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(response);
        }

        // GET: Response/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Response.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }

            List<string> projects = new List<string>();

            foreach(Project row in db.Project)
            {
                projects.Add(row.Name);
            }

            ViewBag.Projects = projects;
            if (response.Project.Name != null)
                ViewBag.Current = response.Project.Name;
            return PartialView("Edit", response);
        }

        // POST: Response/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit([Bind(Include = "ResponseId,FirstName,LastName,Email,PhoneNumber,Checked,UserId")] Response response, string name)
        {
            var project = db.Project.Where(r => r.Name == name);
            if (ModelState.IsValid && project.Count() == 1)
            {
                response.ProjectId = project.First().ProjectId;
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
            }
            return JsonConvert.SerializeObject(db.Response.Where(r => r.ResponseId == response.ResponseId), Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        // GET: Response/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Response.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Response/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Response.Find(id);
            db.Response.Remove(response);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Check(int? id)
        {
            if (id != null)
            {
                Response response = db.Response.Find(id);
                if (response != null)
                {
                    response.Checked = !response.Checked;
                    db.SaveChanges();
                }
                else
                {
                    return HttpNotFound();
                }
                return Json (new { check = response.Checked } );
            }
            return null;
        }

        [HttpGet]
        public Response getResponse(int? id)
        {
            return db.Response.Find(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
