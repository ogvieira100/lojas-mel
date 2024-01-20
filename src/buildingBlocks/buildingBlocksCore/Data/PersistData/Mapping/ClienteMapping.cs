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
    public class ClientesMapping : BaseMapping<Cliente>
    {

        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.CPF)
                .HasColumnName("CPF")
                .HasMaxLength(11);


            builder.Property(e => e.Nome)
               .HasColumnName("Nome")
               .HasMaxLength(200);

            builder.Property(e => e.Email)
            .HasColumnName("Email")
            .HasMaxLength(200);

            builder.ToTable("Cliente");

        }
        
    }
}
