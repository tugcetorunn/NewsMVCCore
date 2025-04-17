using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCCore11IdentityUygulama.Models.Configurations
{
    public class KategoriCFG : IEntityTypeConfiguration<Kategori>
    {
        public void Configure(EntityTypeBuilder<Kategori> builder)
        {
            builder.Property(x => x.KategoriAdi)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Kategori
                {
                    KategoriId = 1,
                    KategoriAdi = "Spor"
                },
                new Kategori
                {
                    KategoriId = 2,
                    KategoriAdi = "Magazin"
                }, new Kategori
                {
                    KategoriId = 3,
                    KategoriAdi = "Eğitim"
                }, new Kategori
                {
                    KategoriId = 4,
                    KategoriAdi = "Kültür"
                });
        }
    }
}
