using Question04CascadedDropdown.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Models.Q4models
{

    public enum Gender1
    {
        Male,
        Female,
        None
    }

    [Table("Individual")]
    public class Individual
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DOBValidation(ErrorMessage = "You must be at least 18 years old.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [DisplayName("City")]
        [ForeignKey("City")]
        public int CityID { get; set; }

        [DisplayName("State")]
        [ForeignKey("State")]
        public int StateID { get; set; }

        [DisplayName("Country")]
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
    }
}
