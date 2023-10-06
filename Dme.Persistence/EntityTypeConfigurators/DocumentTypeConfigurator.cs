using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators;

internal class DocumentTypeConfigurator : IEntityTypeConfiguration<DocumentTypeEntity>
{
	public void Configure(EntityTypeBuilder<DocumentTypeEntity> builder)
	{
		builder.HasKey(e => e.Id);
	}
}