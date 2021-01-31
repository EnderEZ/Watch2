using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities {
    public class Post {
        [Key]
        [ForeignKey ("Product")]
        public int ProductID { get; set; }
        public string PostContent { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [StringLength (250)]
        public string SeoTitle { get; set; }

        [StringLength (350)]
        public string SeoDescritption { get; set; }

        //Nav property
        public List<Product> Products { get; set; }
    }
}