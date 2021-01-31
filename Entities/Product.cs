using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities {
    public class Product {
        [Key]
        public int ID { get; set; }

        [MaxLength (50)]
        public string Name { get; set; }
        public bool? isShow { get; set; }
        public bool? isDel { get; set; }
        public int Price { get; set; }
        public int SaleCount { get; set; }

        [StringLength (50)]
        public string Image { get; set; }

        [ForeignKey ("Category")]
        public int CategoryID { get; set; }

        [ForeignKey ("TypeWire")]
        public int TypeWireID { get; set; }

        [ForeignKey ("Band")]
        public int BandID { get; set; }
        //Nav property
        public Band Band { get; set; }
        public Category Category { get; set; }
        public TypeWire TypeWire { get; set; }
        public ProductDetail ProductDetail { get; set; }

    }
}