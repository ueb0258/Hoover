using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hoover.Models;

namespace Hoover.Controllers
{
    public class EquipmentController : Controller
    {
        private HOOVEREntities db = new HOOVEREntities();

        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            return View(db.Equipment.ToList());
        }

        //
        // GET: /Equipment/Details/5

        public ActionResult Details(int id = 0)
        {
            Equipment equipment = db.Equipment.Single(e => e.EquipmentId == id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // GET: /Equipment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Equipment/Create

        [HttpPost]
        public ActionResult Create(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipment.AddObject(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        //
        // GET: /Equipment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Equipment equipment = db.Equipment.Single(e => e.EquipmentId == id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // POST: /Equipment/Edit/5

        [HttpPost]
        public ActionResult Edit(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipment.Attach(equipment);
                db.ObjectStateManager.ChangeObjectState(equipment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        //
        // GET: /Equipment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Equipment equipment = db.Equipment.Single(e => e.EquipmentId == id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // POST: /Equipment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipment.Single(e => e.EquipmentId == id);
            db.Equipment.DeleteObject(equipment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}