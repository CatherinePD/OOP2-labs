using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.DataAccess.Entities
{
    public class Lot : BaseEntity
    {

        [Required]
        [StringLength(75)]
        public string Title { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateToExpire { get; set; }

        public DateTime? DateFinished { get; set; }

        public decimal StartBid { get; set; }

        public decimal CurrentBid { get; set; }

        public decimal MinBidStep { get; set; }

        public int ProductId { get; set; }

        public bool IsClosed { get; set; }

        public bool IsExpired { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
