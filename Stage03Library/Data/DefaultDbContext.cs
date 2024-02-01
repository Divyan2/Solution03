using Stage03Library.Models;
using Stage03Library.Models.Q4models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Hierarchy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext() : base("DBCS")
        {
            Database.SetInitializer<DefaultDbContext>(null);
        }

        public DbSet<Person> PersonContext { get; set; }
        public DbSet<UserModel> UserModelContext { get; set; }
        public DbSet<SecurityQuestionTable> SecurityQuestionTableContext { get; set;}
        public DbSet<Individual> IndividualsContext { get; set; }
        public DbSet<Country> CountryContext { get; set; }
        public DbSet<State> StateContext { get; set; }
        public DbSet<City> CityContext { get; set; }
    }
}
