using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class Project
    {
        [Key]
        public int pid { get; set; }

      
        public int info_id { get; set; }
        public string? project_name { get; set; }

        [Required]
        public string? Tech_stack { get; set; }

     
        public string? Descritpion { get; set; }

        public string? Link { get; set; }
    }
}
