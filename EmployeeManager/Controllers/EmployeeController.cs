using EmployeeManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var EmpList = db.AllEmployees.ToList().Select(emp =>
                {
                    emp.User = db.Users.FirstOrDefault((user => user.Id == emp.ApplicationUserId));
                    return emp;
                }).ToList();
                return View(EmpList);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var e = db.AllEmployees.Where(c => c.ApplicationUserId == userId).First();
                return View("Details", e);
            }
        }
 
        // GET: Employee/Details/
        [Authorize]
        public ActionResult Details(int id)
        {
            var currentEmployee = db.AllEmployees.Find(id);
            return View(currentEmployee);
        }

        // GET: Employee/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var e = db.AllEmployees.Find(id);
            return View(e);
        }

        // POST: Employee/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, EmployeeActiveModel empActive)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (empActive != null)
                    {
                        if (empActive.IsActive)
                            UserManager.SetLockoutEndDate(empActive.UserId, new DateTimeOffset(DateTime.Now.AddSeconds(0)));
                        else
                            UserManager.SetLockoutEndDate(empActive.UserId, new DateTimeOffset(DateTime.Now.Add(UserManager.DefaultAccountLockoutTimeSpan).ToUniversalTime(), TimeSpan.FromDays(0)));
                    }
                }
                
                // TODO: Add update logic here
                var e = db.AllEmployees.Find(id);
                UpdateModel(e);
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        { 
            var currentEmployee = db.AllEmployees.Find(id);
            db.AllEmployees.Remove(currentEmployee);
            db.SaveChanges();

            return View("Index", db.AllEmployees.ToList());
        }
        
        public ActionResult Lockout()
        {
            return View();
        }
    }
}
