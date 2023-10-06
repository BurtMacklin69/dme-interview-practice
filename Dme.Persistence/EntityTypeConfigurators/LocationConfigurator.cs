using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class LocationConfigurator : IEntityTypeConfiguration<LocationEntity>
{
	public void Configure(EntityTypeBuilder<LocationEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasOne<AddressEntity>().WithMany().HasForeignKey(e => e.AddressId);
		builder.HasOne<CoordinatesEntity>().WithMany().HasForeignKey(e => e.CoordinatesId);
	}
}