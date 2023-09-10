using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resume.Models;

namespace Resume.Data
{
    public class ResumeContext : DbContext
    {
        internal object Infoes;

        public ResumeContext (DbContextOptions<ResumeContext> options)
            : base(options)
        {
            Infoes = new object();
        }

        public DbSet<Resume.Models.Info> Info { get; set; } = default!;

        public DbSet<Resume.Models.Certification>? Certifications { get; set; }

        public DbSet<Resume.Models.Educations> Education { get; set; } = default!;

        public DbSet<Resume.Models.Experience>? Experience { get; set; }

        public DbSet<Resume.Models.Project>? Projects { get; set; }

        public DbSet<Resume.Models.References> Reference { get; set; } = default!;

        public DbSet<Resume.Models.Scholarships>? Scholarship { get; set; }

        public DbSet<Resume.Models.Skills> Skills { get; set; } = default!;
    }
}
