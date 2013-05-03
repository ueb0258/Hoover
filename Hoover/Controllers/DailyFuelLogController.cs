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
    public class DailyFuelLogController : Controller
    {
        private HOOVEREntities db = new HOOVEREntities();

        //
        // GET: /DailyFuelLog/

        public ActionResult Index()
        {
            var dailyfuellog = db.DailyFuelLog.Include("Equipment");
            return View(dailyfuellog.ToList());
        }

        public ActionResult Report(int equipmentId = 0)
        {
            Equipment equip = new Equipment();

            equip = (from eq in db.Equipment
                     select eq).Where(e => e.EquipmentId == equipmentId).FirstOrDefault();

            var eqname = equip.EquipmentNumber + ' ' + equip.Model;

            var dailyfuellog = db.DailyFuelLog.Include("Equipment").Where(dfl => dfl.EquipmentId == equip.EquipmentId);

            ViewBag.eqName = eqname;

            if (dailyfuellog.Count() == 0)
            {
                ViewBag.noLogs = "No Logs To Display";
            }

            return View(dailyfuellog.ToList());

        }

        //
        // GET: /DailyFuelLog/Details/5

        public ActionResult Details(long id = 0)
        {
            DailyFuelLog dailyfuellog = db.DailyFuelLog.Single(d => d.DailyFueLogId == id);
            if (dailyfuellog == null)
            {
                return HttpNotFound();
            }
            return View(dailyfuellog);
        }

        //
        // GET: /DailyFuelLog/Create

        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipment, "EquipmentId", "EquipmentNumber");
            return View();
        }

        //
        // POST: /DailyFuelLog/Create

        [HttpPost]
        public ActionResult Create(DailyFuelLog dailyfuellog)
        {
            if (ModelState.IsValid)
            {
                db.DailyFuelLog.AddObject(dailyfuellog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipment, "EquipmentId", "EquipmentNumber", dailyfuellog.EquipmentId);
            return View(dailyfuellog);
        }

        //
        // GET: /DailyFuelLog/Edit/5

        public ActionResult Edit(long id = 0)
        {
            DailyFuelLog dailyfuellog = db.DailyFuelLog.Single(d => d.DailyFueLogId == id);
            if (dailyfuellog == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipment, "EquipmentId", "EquipmentNumber", dailyfuellog.EquipmentId);
            return View(dailyfuellog);
        }

        //
        // POST: /DailyFuelLog/Edit/5

        [HttpPost]
        public ActionResult Edit(DailyFuelLog dailyfuellog)
        {
            if (ModelState.IsValid)
            {
                db.DailyFuelLog.Attach(dailyfuellog);
                db.ObjectStateManager.ChangeObjectState(dailyfuellog, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipment, "EquipmentId", "EquipmentNumber", dailyfuellog.EquipmentId);
            return View(dailyfuellog);
        }

        //
        // GET: /DailyFuelLog/Delete/5

        public ActionResult Delete(long id = 0)
        {
            DailyFuelLog dailyfuellog = db.DailyFuelLog.Single(d => d.DailyFueLogId == id);
            if (dailyfuellog == null)
            {
                return HttpNotFound();
            }
            return View(dailyfuellog);
        }

        //
        // POST: /DailyFuelLog/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            DailyFuelLog dailyfuellog = db.DailyFuelLog.Single(d => d.DailyFueLogId == id);
            db.DailyFuelLog.DeleteObject(dailyfuellog);
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