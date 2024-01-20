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
    public class PedidosMapping : BaseMapping<Pedido>
    {
        public override void Configure(EntityTypeBuilder<Pedido> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Observacao)
                   .HasColumnName("Observacao")
                   .HasMaxLength(int.MaxValue)
                   .IsRequired(false)
                   ;

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Pedidos)
                .HasForeignKey(x => x.ClienteId);

            builder.HasOne(x => x.Fornecedor)
               .WithMany(x => x.Pedidos)
               .HasForeignKey(x => x.FornecedorId);

            builder.ToTable("Pedido");
        }
    }
}
