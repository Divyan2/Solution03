using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Models.Q4models
{
    [Table("City")]
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        [DisplayName("State")]
        [ForeignKey("State")]
        public int StateID { get; set; }
        public virtual State State { get; set; }
    }
}
