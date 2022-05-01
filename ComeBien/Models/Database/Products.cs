using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Database
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(200)]
        public string EsName { get; set; }

        [Required, StringLength(200)]
        public string EnName { get; set; }

        [StringLength(200)]
        public string FrName { get; set; }

        [Required, StringLength(1000)]
        public string EsDescription { get; set; }

        [Required, StringLength(1000)]
        public string EnDescription { get; set; }

        [StringLength(1000)]
        public string FrDescription { get; set; }

        [Column(TypeName ="money")]
        public decimal Price { get; set; }
    }
}
