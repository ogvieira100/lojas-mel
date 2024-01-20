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
    public class FornecedorMapping : BaseMapping<Fornecedor>
    {
        public override void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CNPJ)
               .HasColumnName("CNPJ")
               .HasMaxLength(14);


            builder.Property(e => e.RazaoSocial)
               .HasColumnName("RazaoSocial")
               .HasMaxLength(200);

            builder.Property(e => e.Email)
             .HasColumnName("Email")
             .HasMaxLength(200);

            builder.ToTable("Fornecedor");
        }
    }
}
