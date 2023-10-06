using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class NameConfigurator : IEntityTypeConfiguration<NameEntity>
{
	public void Configure(EntityTypeBuilder<NameEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasOne<TitleEntity>().WithMany().HasForeignKey(e => e.TitleId);
		builder.HasOne<UserEntity>().WithOne().HasForeignKey<UserEntity>(e => e.NameId);
	}
}