using Stage03Library.Models.Q4models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Interface
{
    public interface IQuestion04Repo
    {
        //CRUD
        List<Individual> GetIndividuals();
        Individual GetIndividualById(int id);
        void AddIndividual(Individual individual);
        void UpdateIndividual(Individual individual);
        void DeleteIndividual(int id);

        //Cascading dropdown
        List<Country> GetCountries();
        List<State> GetStatesByCountryId(int countryId);
        List<City> GetCitiesByStateId(int stateId);
    }
}
