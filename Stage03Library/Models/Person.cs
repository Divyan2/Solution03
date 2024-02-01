using Question04CascadedDropdown.Models;
using Stage03Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stage03Library.Models
{
    [Table("peopletable")]
    public class Person
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Column("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [Column("Gender")]
        public string Gender { get; set; }
        [DOBValidation(ErrorMessage = "Age must be 18 years or older.")]
        [Required(ErrorMessage = "DOB is required")]
        [Column("DOB")]
        public DateTime DOB { get; set; }
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }
        [Column("Updated_On")]
        public DateTime? UpdatedOn { get; set;}
        [Column("Created_By")]
        public long? CreatedBy { get; set; }
        [Column("Updated_By")]
        public long? UpdatedBy { get;set; }
    }
}
