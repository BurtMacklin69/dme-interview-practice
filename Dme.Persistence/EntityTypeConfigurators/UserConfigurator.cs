using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class UserConfigurator : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasMany(e => e.Documents).WithOne().HasForeignKey(e => e.UserId);
		builder.HasMany(e => e.Pictures).WithOne().HasForeignKey(e => e.UserId);
	}
}