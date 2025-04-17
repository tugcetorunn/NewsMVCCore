using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCCore11IdentityUygulama.Models.Configurations
{
    // 4. konfigürasyon sınıflarının oluşturulması
    public class EditorCFG : IEntityTypeConfiguration<Editor>
    {
        public void Configure(EntityTypeBuilder<Editor> builder)
        {
            builder.Property(x => x.Ad)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Soyad)
                .IsRequired()
                .HasMaxLength(50);

            // 5. veritabanı başlangıç editörün oluşturulması

            Editor editor = new Editor
            {
                Id = 1,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Ad = "Pelin",
                Soyad = "Yılmaz",
                Email = "py@hotmail.com",
                NormalizedEmail = "PY@HOTMAIL.COM",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // 6. şifre ayarları

            var passwordHasher = new PasswordHasher<Editor>();
            editor.PasswordHash = passwordHasher.HashPassword(editor, "Admin*123");
            builder.HasData(editor); // bu yazdıklarımız üye tablosunda olacak rol atama ilişki tablosunda olacağı için burada yapmıyoruz.

        }
    }
}
