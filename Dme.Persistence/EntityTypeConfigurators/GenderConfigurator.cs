using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class GenderConfigurator : IEntityTypeConfiguration<GenderEntity>
{
	public void Configure(EntityTypeBuilder<GenderEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasOne<UserEntity>().WithOne().HasForeignKey<UserEntity>(e => e.GenderId);
	}
}