using Autofac;
using Dme.Persistence.Extensions;
using Dme.Persistence.Pattern;

namespace Dme.Persistence.Infrastructure;

public class PersistenceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		foreach (var repository in ThisAssembly.DefinedTypes.Where(type => type.IsInheritorOfGeneric(typeof(Repository<>))))
			builder.RegisterType(repository).AsImplementedInterfaces();
	}
}