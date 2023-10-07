using System.Linq.Expressions;

namespace Dme.Interaction.RepositoryRequests.Models;

public class QueryOptions
{
	public QueryOptions(int? pageNumber, int? pageSize, bool? ascending = null) 
	{
		if (pageNumber == 0)
			throw new ArgumentException($"{nameof(pageNumber)} must be positive or absent", nameof(pageNumber));
		if (pageSize == 0)
			throw new ArgumentException($"{nameof(pageSize)} must be positive or absent", nameof(pageSize));
		if (new[] { pageNumber, pageSize }.Count(b => b > 0) == 1)
			throw new ArgumentException($"{nameof(pageNumber)} and {nameof(pageSize)} must be both positive or absent");

		PageNumber = pageNumber;
		PageSize = pageSize;
		Ascending = ascending ?? true;
	}

	public int? PageNumber { get; }
	public int? PageSize { get; }
	public bool Ascending { get; }

	/// <exception cref="ArgumentNullException">Thrown when <see cref="sortingSelectors"/> is null or empty</exception>
	public IQueryable<TEntity> Apply<TEntity, TSorting>(
		IQueryable<TEntity> query, 
		params Expression<Func<TEntity, TSorting>>[] sortingSelectors)
	{
		query = ToOrdered(query, sortingSelectors ??
		                         throw new ArgumentNullException(nameof(sortingSelectors)));

		if (PageNumber != null)
			query = ToPaginated(query);

		return query;
	}

	private IQueryable<TEntity> ToOrdered<TEntity, TSorting>(
		IQueryable<TEntity> query,
		params Expression<Func<TEntity, TSorting>>[] sortingSelectors)
	{
		var sortedQuery = Ascending
			? query.OrderBy(sortingSelectors.First())
			: query.OrderByDescending(sortingSelectors.First());

		foreach (var sortingSelector in sortingSelectors.Skip(1))
			sortedQuery = Ascending
				? sortedQuery.ThenBy(sortingSelector)
				: sortedQuery.ThenByDescending(sortingSelector);

		return sortedQuery;
	}

	private IQueryable<TEntity> ToPaginated<TEntity>(IQueryable<TEntity> query) =>
		query.Skip(PageSize!.Value * (PageNumber!.Value - 1)).Take(PageSize.Value);
}