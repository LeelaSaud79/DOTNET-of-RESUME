using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class Educations
    {
        [Key]
        public int eid { get; set; }

        [ForeignKey("Info")]
        public int info_id { get; set; }
        [Required]
        public string? College_name { get; set; }

        [Required]
        public DateTime startdate { get; set; }

        public DateTime end_date { get; set; }

        [Required]
        public string? Degree { get; set; }

        [Required]
        public string? Board { get; set; }
    }
}
