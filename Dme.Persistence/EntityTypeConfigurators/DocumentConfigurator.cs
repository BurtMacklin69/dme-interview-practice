using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class DocumentConfigurator : IEntityTypeConfiguration<DocumentEntity>
{
	public void Configure(EntityTypeBuilder<DocumentEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}