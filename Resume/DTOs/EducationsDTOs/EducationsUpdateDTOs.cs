namespace Resume.DTOs.EducationsDTOs
{
    public class EducationsUpdateDTOs
    {

        public int info_id { get; set; }
        public int eid { get; set; }

        public string? College_name { get; set; }

        public DateTime startdate { get; set; }

        public DateTime end_date { get; set; }

        public string? Degree { get; set; }

        public string? Board { get; set; }
    }
}
