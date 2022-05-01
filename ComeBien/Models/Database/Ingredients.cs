using ComeBien.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Database
{
    public class Ingredients : NotificationClass
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string EsName { get; set; }

        [Required, StringLength(200)]
        public string EnName { get; set; }

        [StringLength(200)]
        public string FrName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }


    }
}
