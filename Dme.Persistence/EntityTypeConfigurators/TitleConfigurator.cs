using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class TitleConfigurator : IEntityTypeConfiguration<TitleEntity>
{
	public void Configure(EntityTypeBuilder<TitleEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}