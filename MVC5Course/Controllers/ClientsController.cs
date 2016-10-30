using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using MVC5Course.Controllers;

namespace MVC5Course.Controllers
{
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        // GET: Clients
        //public ActionResult Index()
        //{
        //    var client = db.Client.Include(c => c.Occupation);

        //    client.OrderByDescending(p => p.ClientId);

        //    return View(client.Take(10).ToList());
        //}

        public ActionResult Index(string search, int? CreditRating, string Gender)
        {
            var client = db.Client.Include(c => c.Occupation);
            if (!string.IsNullOrEmpty(search))
            {
                client = client.Where(x => x.FirstName.Contains(search));
                //client = client.Where(x => x.FirstName == search);
            }

            client = client.OrderByDescending(p => p.ClientId).Take(10);

            var option = db.Client.Select(p => p.CreditRating).Distinct().OrderBy(p => p).ToList();

            ViewBag.CreditRating = new SelectList(option);
            ViewBag.Gender = new SelectList(new string[] { "M", "F" });

            return View(client);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ClientLoginViewModel model)
        {
            return View("LoginResult", model);
        }
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection from)
        {
            var client = db.Client.Find(id);
            //檢核modelbinding資料
            if (TryUpdateModel(client))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //if (ModelState.IsValid)
            //{

            //db.Entry(client).State = EntityState.Modified;
            //db.SaveChanges();

            //}
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
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
