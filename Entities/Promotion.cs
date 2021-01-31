using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities {
    public class Promotion {
        [Key]
        public int ID { get; set; }

        [StringLength (50)]
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? Status { get; set; }
        public int Type { get; set; }
    }
}