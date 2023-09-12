namespace Resume.DTOs.CertificationsDTOs
{
    public class CertificationsUpdateDTOs
    {
        public int cid { get; set; }

        public int info_id { get; set; }
        public string? title { get; set; }


        public DateTime issue_date { get; set; }

        public string? institute { get; set; }

        public string? link { get; set; }
    }
}
