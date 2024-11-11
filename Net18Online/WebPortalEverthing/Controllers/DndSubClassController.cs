﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers
{
    public class DndSubClassController : Controller
    {
        // GET: DndSubClassController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DndSubClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DndSubClassController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DndSubClassController/Create
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

        // GET: DndSubClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DndSubClassController/Edit/5
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

        // GET: DndSubClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DndSubClassController/Delete/5
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
