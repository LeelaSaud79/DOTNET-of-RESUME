using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class Experience
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Info")]
        public int info_id { get; set; }

        [Required]
        public string? Company_name { get; set; }

        [Required]
        public string? Position { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

        [Required]
        public string? Tech_used { get; set; }
    }
}
