using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class AddressConfigurator : IEntityTypeConfiguration<AddressEntity>
{
	public void Configure(EntityTypeBuilder<AddressEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}