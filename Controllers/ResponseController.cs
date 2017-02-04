using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Practice.Models
{
    public class ResponseController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Responses
        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.Projects).Include(r => r.Users);
            var join = responses.Join(responses, user => user, response => response, (user, response) => new { userId = user.Id, responseId = response.UserId });
            return View(responses.ToList());
        }

        // GET: Responses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responses response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: Responses/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,PhoneNumber,Email,Checked")] Responses response)
        {

            string statement = "SELECT * FROM dbo.Users WHERE Email = " + "'" + response.Email + "'" + "OR PhoneNumber = " + "'" + response.PhoneNumber + "'";
            var check = db.Users.SqlQuery(statement).ToList();


            if (!check.Any())
            {
                response.UserId = null;
            }
            else
            {
                Users user = check.First();
                response.UserId = user.Id;
            }

            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", response.ProjectId);
            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responses response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", response.ProjectId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", response.UserId);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserId,ProjectId,FirstName,LastName,PhoneNumber,Email,Checked")] Responses response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", response.ProjectId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", response.UserId);
            return View(response);
        }

        // GET: Responses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responses response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Responses response = db.Responses.Find(id);
            db.Responses.Remove(response);
            db.SaveChanges();
            return RedirectToAction("Index");
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
