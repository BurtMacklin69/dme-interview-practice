using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence;

public class UsersDbContext : DbContext
{
	private readonly Action<DbContextOptionsBuilder> _builderAction;

	public UsersDbContext()
	{
		_builderAction = ctx =>
			ctx.UseSqlServer(
				"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DmeLupinovUsers;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
	}

	public UsersDbContext(Action<DbContextOptionsBuilder> builderAction) =>
		_builderAction = builderAction ??
		                 throw new ArgumentNullException(nameof(builderAction));

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _builderAction(optionsBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder) =>
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

	public DbSet<UserEntity> Users { get; set; } = null!;
}