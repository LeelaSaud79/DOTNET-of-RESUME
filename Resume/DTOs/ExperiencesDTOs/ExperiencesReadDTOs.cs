using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.DTOs.ExpereincesDTOs
{
    public class ExperiencesReadDTOs
    {
        public int id { get; set; }

        public int info_id { get; set; }

       
        public string? Company_name { get; set; }

       
        public string? Position { get; set; }

        
        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

        public string? Tech_used { get; set; }
    }
}
