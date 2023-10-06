namespace Dme.Persistence.Extensions
{
	internal static class TypeExtensions
	{
		public static bool IsInheritorOfGeneric(this Type type, Type ofGeneric)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (ofGeneric == null) throw new ArgumentNullException(nameof(ofGeneric));

			if (!ofGeneric.IsGenericType)
				return false;

			ofGeneric = ofGeneric.GetGenericTypeDefinition();
			if (type.IsGenericType && type.GetGenericTypeDefinition() == ofGeneric)
				return false;

			while (type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == ofGeneric)
					return true;

				type = type.BaseType!;
			}

			return false;
		}
	}
}
