using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using web_asignment1_Y2S1_2022.Models;

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class SalesManagerController : Controller
    {
        // GET: SalesManagerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalesManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesManagerController/Create
        public ActionResult Create()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: SalesManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
