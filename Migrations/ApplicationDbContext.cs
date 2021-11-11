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
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
<<<<<<< HEAD
        public DbSet<Cita> Citas { get; set; }
=======
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }

>>>>>>> cc92caf5834c7b4d9d69b211859ded9477cf8b61
    }

}
