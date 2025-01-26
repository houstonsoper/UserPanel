using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using userpanel.api.Models;

namespace userpanel.api.Configurations;

public class PasswordResetTokenEntityTypeConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder
            .HasOne(prt => prt.User)
            .WithMany()
            .HasForeignKey(prt => prt.UserId);
    }
}