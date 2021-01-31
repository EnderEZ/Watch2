using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities {
    public class PromProduct {
        [Key]
        [ForeignKey ("Promotion")]
        public int PromotionID { get; set; }

        [StringLength (50)]
        public string Discount { get; set; }

        [StringLength (250)]
        public string ProductIDs { get; set; }
        public int? CategoryID { get; set; }
        public int? BandID { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }
}