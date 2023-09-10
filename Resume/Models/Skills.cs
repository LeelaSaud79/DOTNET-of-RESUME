using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.Models
{
    public class Skills
    {
        [Key]
        public int skill_id { get; set; }

        public int info_id { get; set; }
        
        public string? skill { get; set; }
    }
}
