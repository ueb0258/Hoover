using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hoover.Models;

namespace Hoover.Controllers
{
    public class EquipmentController : Controller
    {
        //
        // GET: /Equipment/

        private HOOVEREntities db = new HOOVEREntities();

        public ViewResult Index()
        {
            return View(db.Equipment.ToList());
        }
    }
}
