using System.ComponentModel.DataAnnotations.Schema;

namespace Resume.DTOs.ReferencesDTOs
{
    public class ReferencesCreateDTOs
    {
       
        public int info_id { get; set; }
        
        public string? reference_by { get; set; }

       
        public string? company { get; set; }

        
        public string? email { get; set; }

        public string? phone_number { get; set; }

        public string? designation { get; set; }
    }
}
