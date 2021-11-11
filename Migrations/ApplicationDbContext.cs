using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GYMIMFC.Models;


namespace GYMIMFC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Matricula> Matricula { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }

    }

}
