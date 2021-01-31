using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class Category {
        [Key]
        public int ID { get; set; }

        [MaxLength (30)]
        public string Name { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [MaxLength (150)]
        public string SeoTitle { get; set; }

        [MaxLength (350)]
        public string SeoDescription { get; set; }

        //Nav property
        public List<Product> Products { get; set; }
    }
}