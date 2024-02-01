using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Models.Q4models
{
    [Table("Country")]
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
