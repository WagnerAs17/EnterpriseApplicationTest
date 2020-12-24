using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volvo.Frota.API.Models;
namespace Volvo.Frota.API.Data.Mappings
{
    public class CaminhaoMapping : IEntityTypeConfiguration<Caminhao>
    {
        public void Configure(EntityTypeBuilder<Caminhao> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Modelo)
                .IsRequired()
                .HasColumnType("char(2)");

            builder.Property(p => p.AnoModelo)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.AnoFabricacao)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("Caminhoes");
        }
    }
}
