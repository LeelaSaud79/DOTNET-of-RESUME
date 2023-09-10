using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class References
    {
        [Key]
        public int ref_id { get; set; }

        [ForeignKey("Info")]
        public int info_id { get; set; }
        [Required]
        public string? reference_by { get; set; }

        [Required]
        public string? company { get; set; }

        [Required]
        public string? email { get; set; }

        public string? phone_number { get; set; }

        public string? designation { get; set; }
    }
}
