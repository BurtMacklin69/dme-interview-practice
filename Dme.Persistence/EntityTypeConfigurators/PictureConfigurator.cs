using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class PictureConfigurator : IEntityTypeConfiguration<PictureEntity>
{
	public void Configure(EntityTypeBuilder<PictureEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}