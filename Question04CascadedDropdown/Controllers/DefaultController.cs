using Stage03Library.Interface;
using Stage03Library.Models.Q4models;
using Stage03Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Question04CascadedDropdown.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IQuestion04Repo repo;
        public DefaultController()
        {
            repo = new Question04Repo();
        }
        public DefaultController(IQuestion04Repo repo)
        {

            this.repo = repo;
        }
        // GET: Default
        public ActionResult Index()
        {
            var individuals = repo.GetIndividuals();
            return View(individuals);
        }

        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(repo.GetCountries(), "CountryID", "CountryName");
            ViewBag.States = new SelectList(new List<State>(), "StateID", "StateName");
            ViewBag.Cities = new SelectList(new List<City>(), "CityID", "CityName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Individual individual)
        {
            if (ModelState.IsValid)
            {
                repo.AddIndividual(individual);
                return RedirectToAction("Index");
            }

            ViewBag.Countries = new SelectList(repo.GetCountries(), "CountryID", "CountryName");
            ViewBag.States = new SelectList(new List<State>(), "StateID", "StateName");
            ViewBag.Cities = new SelectList(new List<City>(), "CityID", "CityName");

            return View(individual);
        }

        public ActionResult Edit(int id)
        {
            var individual = repo.GetIndividualById(id);
            if (individual == null)
            {
                return HttpNotFound();
            }

            ViewBag.Countries = new SelectList(repo.GetCountries(), "CountryID", "CountryName", individual.CountryID);
            ViewBag.States = new SelectList(repo.GetStatesByCountryId(individual.CountryID), "StateID", "StateName", individual.StateID);
            ViewBag.Cities = new SelectList(repo.GetCitiesByStateId(individual.StateID), "CityID", "CityName", individual.CityID);

            return View(individual);
        }

        [HttpPost]
        public ActionResult Edit(Individual individual)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateIndividual(individual);
                return RedirectToAction("Index");
            }

            ViewBag.Countries = new SelectList(repo.GetCountries(), "CountryID", "CountryName", individual.CountryID);
            ViewBag.States = new SelectList(repo.GetStatesByCountryId(individual.CountryID), "StateID", "StateName", individual.StateID);
            ViewBag.Cities = new SelectList(repo.GetCitiesByStateId(individual.StateID), "CityID", "CityName", individual.CityID);

            return View(individual);
        }

        public ActionResult Details(int id)
        {
            var individual = repo.GetIndividualById(id);

            if (individual == null)
            {
                return HttpNotFound();
            }

            return View(individual);
        }


        public ActionResult Delete(int id)
        {
            var individual = repo.GetIndividualById(id);
            if (individual == null)
            {
                return HttpNotFound();
            }

            return View(individual);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteIndividual(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetStatesByCountryId(int CountryID)
        {
            var states = repo.GetStatesByCountryId(CountryID);
            return Json(states,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCitiesByStateId(int stateId)
        {
            var cities = repo.GetCitiesByStateId(stateId);
            return Json(cities);
        }
    }
}