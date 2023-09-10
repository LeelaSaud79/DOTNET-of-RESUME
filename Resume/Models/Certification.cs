using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Resume.Models
{
    public class Certification
    {
        [Key]
        public int cid { get; set; }
        public int info_id { get; set; }

        public string? title { get; set; }

        
        public DateTime issue_date { get; set; }

        public string? institute { get; set; }

        public string? link { get; set; }
    }
}
