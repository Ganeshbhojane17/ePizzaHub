using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Domain.Models
{
    public class ItemDomain
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; } = default!;

        public int CategoryId { get; set; }

        public int ItemTypeId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
