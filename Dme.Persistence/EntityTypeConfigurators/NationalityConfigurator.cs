using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class NationalityConfigurator : IEntityTypeConfiguration<NationalityEntity>
{
	public void Configure(EntityTypeBuilder<NationalityEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasOne<UserEntity>().WithOne().HasForeignKey<UserEntity>(e => e.NationalityId);
	}
}