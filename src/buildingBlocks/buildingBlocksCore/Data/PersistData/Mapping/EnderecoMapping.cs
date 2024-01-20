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
    public class EnderecoMapping : BaseMapping<Endereco>
    {
        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Estado)
                .HasColumnName("Estado")
                ;

            builder.Property(e => e.TipoEndereco)
               .HasColumnName("TipoEndereco")
               ;

     
            builder.Property(e => e.Logradouro)
               .HasColumnName("Logradouro")
               .HasMaxLength(200)
               ;

            builder.Property(e => e.Bairro)
              .HasColumnName("Bairro")
              .HasMaxLength(50)
              ;

            builder.Property(e => e.Numero)
             .HasColumnName("Numero")
             .HasMaxLength(20)
             ;

            builder.Property(e => e.ClienteId)
               .HasColumnName("ClienteId")
               ;

            builder.HasOne(x => x.Cliente)
                  .WithMany(x => x.Enderecos)
                  .HasForeignKey(x => x.ClienteId);

            builder.Property(e => e.FornecedorId)
                .HasColumnName("FornecedorId")
            ;

            builder.HasOne(x => x.Fornecedor)
                  .WithMany(x => x.Enderecos)
                  .HasForeignKey(x => x.FornecedorId);


            builder.ToTable("Endereco");

        }
    }
}
