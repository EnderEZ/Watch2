using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class Policy {
        [Key]
        public string ID { get; set; }

        [StringLength (150)]
        public string PolicyContent { get; set; }

        [StringLength (50)]
        public string Icon { get; set; }
    }
}