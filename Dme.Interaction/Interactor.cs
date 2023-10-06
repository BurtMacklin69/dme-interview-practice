using Microsoft.EntityFrameworkCore;

namespace Dme.Interaction;

internal abstract class Interactor
{
    protected readonly DbContext DbContext;

    protected Interactor(DbContext dbContext) =>
        DbContext = dbContext ??
                    throw new ArgumentNullException(nameof(dbContext));
}