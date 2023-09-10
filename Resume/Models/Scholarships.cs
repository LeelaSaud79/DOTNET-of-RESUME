using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class Scholarships
    {
        [Key]
        public int scholar_id { get; set; }

        [ForeignKey("Info")]
        public int info_id { get; set; }
        public string? scholarship_name { get; set; }

        [Required]
        public DateTime issue { get; set; }

        public string? issue_by { get; set; }

        public string? scholar_name { get; set; }
    }
}
