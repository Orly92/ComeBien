using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Database
{
    public class Administrator
    {
        [Required,StringLength(100)]
        public string UserName { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
