﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;

namespace canteenVLU.Areas.Admin.Controllers
{
    public class ACCOUNTController : Controller
    {
        private QUANLYCANTEENEntities db = new QUANLYCANTEENEntities();

        // GET: Admin/ACCOUNT
        public ActionResult Index()
        {
            return View(db.ACCOUNTs.ToList());
        }

        // GET: Admin/ACCOUNT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ACCOUNT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EMAIL,PASSWORD,FULL_NAME,STATUS,ROLE")] ACCOUNT aCCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.ACCOUNTs.Add(aCCOUNT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // POST: Admin/ACCOUNT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EMAIL,PASSWORD,FULL_NAME,STATUS,ROLE")] ACCOUNT aCCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCOUNT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // POST: Admin/ACCOUNT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            db.ACCOUNTs.Remove(aCCOUNT);
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
