using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class Band {
        [Key]
        public int ID { get; set; }

        [StringLength (30)]
        public string Name { get; set; }
        //Nav property
        public List<Product> Products { get; set; }

    }
}