using Stage03Library.Data;
using Stage03Library.Interface;
using Stage03Library.Models.Q4models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Repository
{
    public class Question04Repo : IQuestion04Repo
    {
        private readonly DefaultDbContext context;
        public Question04Repo()
        {
            context = new DefaultDbContext();
        }
        public Question04Repo(DefaultDbContext context)
        {
            this.context = context;
        }
        public void AddIndividual(Individual individual)
        {
            context.IndividualsContext.Add(individual);
            context.SaveChanges();
        }

        public void DeleteIndividual(int id)
        {
            var individual = context.IndividualsContext.Find(id);
            if (individual != null)
            {
                context.IndividualsContext.Remove(individual);
                context.SaveChanges();
            }
        }

        public List<City> GetCitiesByStateId(int stateId)
        {
            return context.CityContext.Where(c => c.StateID == stateId).ToList();
        }

        public List<Country> GetCountries()
        {
            return context.CountryContext.ToList();
        }

        public Individual GetIndividualById(int id)
        {
            return context.IndividualsContext.Find(id);
        }

        public List<Individual> GetIndividuals()
        {
            return context.IndividualsContext.ToList();
        }

        public List<State> GetStatesByCountryId(int countryId)
        {
            return context.StateContext.Where(s => s.CountryID == countryId).ToList();
        }

        public void UpdateIndividual(Individual individual)
        {
            context.Entry(individual).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
