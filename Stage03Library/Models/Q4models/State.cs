using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Models.Q4models
{
    [Table("State")]
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }

        [DisplayName("Country")]
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
    }
}
