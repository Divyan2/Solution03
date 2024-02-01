using Stage03Library.Data;
using Stage03Library.Interface;
using Stage03Library.Models.Q4models;
using Stage03Library.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Stage03Library.Models;

namespace Question01MVCwithServerSidePagination.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDatabaseRepository _repo;
        public HomeController()
        {
            _repo = new DatabaseRepository(new DefaultDbContext());
        }
        private long GetLoggedInUserId()
        {
            object userId = Session["UserId"];

            if (userId != null && long.TryParse(userId.ToString(), out long result))
            {
                return result;
            }

            return 0;
        }


        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var datalist = _repo.GetAllIndividuals();
            int totalrows = datalist.Count;


            if (!string.IsNullOrEmpty(searchValue))
            {
                datalist = datalist.
                    Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) || x.Gender.ToString().ToLower().Contains(searchValue.ToLower()) || x.DOB.ToString("yyyy-MM-dd").ToLower().Contains(searchValue.ToLower())).ToList();
            }

            int totalrowsafterfiltering = datalist.Count;


            datalist = datalist.OrderBy($"{sortColumnName} {sortDirection}").ToList();

            datalist = datalist.Skip(start).Take(length).ToList();

            var formattedDataList = datalist.Select(x => new
            {
                x.Id,
                x.Name,
                x.Gender,
                DOB = x.DOB.ToString("yyyy-MM-dd"),
                CreatedOn = x.CreatedOn.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedOn = x.UpdatedOn.HasValue ? x.UpdatedOn.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                x.CreatedBy,
                x.UpdatedBy
            });


            return Json(new { data = formattedDataList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            try
            {
                person.CreatedOn = DateTime.Now;
                person.UpdatedOn = DateTime.Now;
                var currentUserId = GetLoggedInUserId();
                person.CreatedBy = currentUserId;
                person.UpdatedBy = currentUserId;

                _repo.AddPerson(person);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var person = _repo.GetIndividualbyId(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            try
            {
                person.UpdatedOn = DateTime.Now;
                var currentUserId = GetLoggedInUserId();
                person.UpdatedBy = currentUserId;

                _repo.UpdatePerson(person);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(long id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var person = _repo.GetIndividualbyId(id);
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repo.DeletePerson(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var person = _repo.GetIndividualbyId(id);
            return View(person);
        }

    }
}