using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Dme.Persistence.Extensions;
using Dme.Persistence.Pattern;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence.Infrastructure
{
	public class PersistenceModule : Module
	{
		private readonly Action<DbContextOptionsBuilder> _builderAction;

		public PersistenceModule(Action<DbContextOptionsBuilder> builder) =>
			_builderAction = builder ?? throw new ArgumentNullException(nameof(builder));

		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(ctx => new UsersDbContext(_builderAction)).As<DbContext>().InstancePerLifetimeScope();
			builder.RegisterAutoMapper(typeof(PersistenceModule).Assembly);

			foreach (var repository in ThisAssembly.DefinedTypes.Where(type => type.IsInheritorOfGeneric(typeof(Repository<>))))
				builder.RegisterType(repository).AsImplementedInterfaces();
		}
	}
}
