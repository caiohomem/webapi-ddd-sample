using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.DomainModelLayer
{
    public class Product : EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
