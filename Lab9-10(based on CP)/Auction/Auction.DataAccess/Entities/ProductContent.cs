using System.ComponentModel.DataAnnotations;

namespace Auction.DataAccess.Entities
{
    public class ProductContent : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
