using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class Info {

        [StringLength (50)]
        public string Logo { get; set; }

        [StringLength (100)]

        public string Email { get; set; }

        [StringLength (100)]

        public string Facebook { get; set; }

        [StringLength (100)]

        public string Instargram { get; set; }

        [StringLength (100)]

        public string Phone { get; set; }

        [StringLength (150)]
        public string Address { get; set; }

        [StringLength (50)]
        public string WorkTime { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [StringLength (250)]
        public string SeoTitle { get; set; }

        [StringLength (350)]
        public string SeoDescritption { get; set; }

    }
}