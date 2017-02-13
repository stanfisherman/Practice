using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice.Models;

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
            return View(response);
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

            Project name = db.Project.Find(id);

            if (name != null)
            {
                ViewBag.Selected = name.Name;
            }

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
            return View(response);
        }

        // POST: Response/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponseId,FirstName,LastName,Email,PhoneNumber,Checked,UserId,ProjectId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(response);
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
        public ActionResult Check(string responseId)
        {
            if (responseId != null)
            {
                Response response = db.Response.Find(Int32.Parse(responseId));
                if (response != null)
                {
                    response.Checked = !response.Checked;
                    db.SaveChanges();
                }
                return Json (new { check = response.Checked } );
            }
            return null;
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
