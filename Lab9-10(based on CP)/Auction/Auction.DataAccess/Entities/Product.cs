using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(75)] public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int OwnerId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductContent> ProductContents { get; set; }

        public virtual User Owner { get; set; }
    }
}
