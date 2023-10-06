using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class CoordinatesConfigurator : IEntityTypeConfiguration<CoordinatesEntity>
{
	public void Configure(EntityTypeBuilder<CoordinatesEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}