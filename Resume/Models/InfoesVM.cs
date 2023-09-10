using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Resume.Models
{
    public class InfoesVM
    {
        public int info_id { get; set; }
        public string? name { get; set; }
        public string? github_link { get; set; }

        public string? address { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? social_media_link { get; set; }
        public string? summary { get; set; }
        public string? designation { get; set; }
    }
}
