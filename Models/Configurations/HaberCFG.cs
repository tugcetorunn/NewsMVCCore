using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCCore11IdentityUygulama.Models.Configurations
{
    public class HaberCFG : IEntityTypeConfiguration<Haber>
    {
        public void Configure(EntityTypeBuilder<Haber> builder)
        {
            builder.Property(x => x.Baslik)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Detay)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.ResimYolu)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
