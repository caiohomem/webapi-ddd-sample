using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.DomainModelLayer
{
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
