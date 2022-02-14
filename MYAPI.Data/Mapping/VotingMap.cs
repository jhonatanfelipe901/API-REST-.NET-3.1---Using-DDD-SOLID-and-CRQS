using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYAPI.Data.Mapping
{
    public class VotingMap : IEntityTypeConfiguration<Voting>
    {
        public void Configure(EntityTypeBuilder<Voting> builder)
        {
            builder.ToTable("Voting");
        }
    }
}
