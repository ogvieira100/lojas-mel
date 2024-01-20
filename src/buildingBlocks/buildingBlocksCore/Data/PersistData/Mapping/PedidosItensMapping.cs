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
    public class PedidosItensMapping : BaseMapping<PedidoItens>
    {
        public override void Configure(EntityTypeBuilder<PedidoItens> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Qtd)
              .HasColumnName("Qtd");

            builder.Property(x => x.Price)
                .HasColumnName("ValorUnitario")
                .HasPrecision(18, 4)
                ;

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.PedidoItens)
                .HasForeignKey(x => x.ProdutoId);

            builder.HasOne(x => x.Pedido)
               .WithMany(x => x.PedidoItens)
               .HasForeignKey(x => x.PedidoId);

            builder.ToTable("PedidoItens");
        }
    }
}
