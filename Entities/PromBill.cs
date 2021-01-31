using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities {
    public class PromBill {
        [Key]
        [ForeignKey ("Promotion")]
        public int PromotionID { get; set; }

        [StringLength (50)]
        public string Discount { get; set; }
        public bool isFeeShip { get; set; }
        public byte ConditionItem { get; set; }
        public int ConditionAmount { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }
}