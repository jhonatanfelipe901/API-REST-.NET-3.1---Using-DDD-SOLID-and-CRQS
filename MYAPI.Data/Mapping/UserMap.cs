using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAPI.Domain.Entities;

namespace MYAPI.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasMany(x => x.Votings)
              .WithOne(r => r.User)
              .HasForeignKey(x => x.UserId);
        }
    }
}
