using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence.Pattern;

public abstract class Repository<T> where T : class
{
	protected readonly DbSet<T> Set;

	protected Repository(DbContext dbContext)
	{
		Set = (dbContext ??
		       throw new ArgumentNullException(nameof(dbContext)))
			.Set<T>();
	}
}