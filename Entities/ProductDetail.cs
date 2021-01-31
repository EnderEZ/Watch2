using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities {
    public class ProductDetail {
        [Key]
        [ForeignKey ("Product")]
        public int ProductID { get; set; }
        public string Images { get; set; }

        [MaxLength (30)]
        public string TypeGlass { get; set; }

        [MaxLength (30)]
        public string TypeBorder { get; set; }

        [MaxLength (30)]
        public string TypeMachine { get; set; }

        [MaxLength (30)]
        public string Parameter { get; set; }

        [MaxLength (30)]
        public string ResistWater { get; set; }

        [MaxLength (30)]
        public string Warranty { get; set; }

        [MaxLength (30)]
        public string Origin { get; set; }

        [MaxLength (30)]
        public string Color { get; set; }

        [MaxLength (30)]
        public string Func { get; set; }

        [MaxLength (1500)]
        public string DescriptionProduct { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [StringLength (250)]
        public string SeoTitle { get; set; }

        [StringLength (350)]
        public string SeoDescritption { get; set; }

        //Nav property
        public Product Product { get; set; }
    }
}