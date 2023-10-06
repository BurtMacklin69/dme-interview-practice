using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dme.Persistence.EntityTypeConfigurators
{
	internal class UserConfigurator : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasMany(e => e.Document).WithOne().HasForeignKey(e => e.UserId);
			builder.HasMany(e => e.Location).WithOne().HasForeignKey(e => e.UserId);
			builder.HasMany(e => e.Picture).WithOne().HasForeignKey(e => e.UserId);
		}
	}
}
