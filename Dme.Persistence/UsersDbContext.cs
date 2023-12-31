﻿using Dme.Persistence.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence;

public class UsersDbContext : DbContext
{
	public const string DatabaseName = "DmeLupinovUsers";

	private readonly Action<DbContextOptionsBuilder> _builderAction;

	public UsersDbContext(Action<DbContextOptionsBuilder> builderAction) =>
		_builderAction = builderAction ??
		                 throw new ArgumentNullException(nameof(builderAction));

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _builderAction(optionsBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder) =>
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

	public DbSet<UserEntity> Users { get; set; } = null!;
}