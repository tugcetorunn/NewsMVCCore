using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCCore11IdentityUygulama.Models;
using System.Reflection;

namespace MVCCore11IdentityUygulama.Data
{
    // 3. context sınıfının oluşturulması
    public class HaberPortalDbContext : IdentityDbContext<Editor, Rol, int>
    {
        public HaberPortalDbContext()
        {
            
        }

        public HaberPortalDbContext(DbContextOptions<HaberPortalDbContext> options) : base(options)
        {

        }

        public DbSet<Haber> Haberler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 8. cfg lerin uygulanması
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // 9. default admin rolü atanması

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                },
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 1
                });
        }
    }
}
