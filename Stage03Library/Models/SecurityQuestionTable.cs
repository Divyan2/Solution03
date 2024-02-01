using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Models
{
    [Table("SecurityQuestionTable")]
    public class SecurityQuestionTable
    {
        [Key]
        public int Code { get; set; }
        public string Question { get; set; }
    }
}
