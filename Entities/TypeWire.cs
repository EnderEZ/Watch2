using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class TypeWire {
        [Key]
        public int ID { get; set; }

        [MaxLength (50)]
        public string Name { get; set; }
        //Nav property
        public List<Product> Products { get; set; }
    }
}