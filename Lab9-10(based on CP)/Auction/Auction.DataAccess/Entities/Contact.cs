using System.ComponentModel.DataAnnotations;

namespace Auction.DataAccess.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        [StringLength(75)]
        public string Email { get; set; }

        [StringLength(75)]
        public string Address { get; set; }

        [StringLength(75)]
        public string Phone { get; set; }

        public string Photo { get; set; }
    }
}
