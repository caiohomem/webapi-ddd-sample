using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.Dto
{
    public class ProductDto : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
