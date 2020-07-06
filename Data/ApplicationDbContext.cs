using System;
using System.Collections.Generic;
using System.Text;
using checking_v2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace checking_v2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
