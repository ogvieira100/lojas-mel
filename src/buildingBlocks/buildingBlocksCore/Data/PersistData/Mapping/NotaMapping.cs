using buildingBlocksCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData.Mapping
{
    public class NotaMapping : BaseMapping<Nota>
    {
        public override void Configure(EntityTypeBuilder<Nota> builder)
        {
           base.Configure(builder); 

            builder.Property(x => x.Numero)
            .HasColumnName("Numero")
            .HasMaxLength(50);

            builder.Property(x => x.Observacao)
           .HasColumnName("Observacao")
           .IsRequired(false)
           .HasMaxLength(int.MaxValue);

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Notas)
                .HasForeignKey(x => x.ClienteId);

            builder.HasOne(x => x.Fornecedor)
                .WithMany(x => x.Notas)
                .HasForeignKey(x => x.FornecedorId);

            builder.ToTable("Nota");

        }
    }
}
