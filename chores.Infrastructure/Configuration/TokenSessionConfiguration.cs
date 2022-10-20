using chores.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chores.Infrastructure.Configuration;

public class TokenSessionConfiguration : IEntityTypeConfiguration<TokenSession>
{
    public void Configure(EntityTypeBuilder<TokenSession> builder)
    {
        builder.ToTable("TokenSessions");
        
        builder.HasKey(t => t.Id);

        builder.Ignore(t => t.ValidUntil);

        builder.HasOne(t => t.User)
            .WithMany(u => u.TokenSessions)
            .HasForeignKey(t => t.UserId);
    }
}