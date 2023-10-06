using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class ContactConfigurator : IEntityTypeConfiguration<ContactEntity>
{
	public void Configure(EntityTypeBuilder<ContactEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasOne<UserEntity>().WithOne(e => e.Contact).HasForeignKey<ContactEntity>(e => e.UserId);
	}
}