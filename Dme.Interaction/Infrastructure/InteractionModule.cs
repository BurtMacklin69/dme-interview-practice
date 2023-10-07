using Autofac;

namespace Dme.Interaction.Infrastructure;

public class InteractionModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		foreach (var interactor in ThisAssembly.DefinedTypes.Where(type => type.IsSubclassOf(typeof(Interactor)) &&
		                                                                   type != typeof(Interactor)))
			builder.RegisterType(interactor).AsImplementedInterfaces();
	}
}