namespace hazped.sharedkernel;

public static class QueryHelper<T> where T : class
{
    public static IQueryable<T> GenerateQuery(IQueryable<T> query, List<string>? includes)
    {
        if (includes is not null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }
        return query;
    }

    public static IQueryable<T> GenerateQuery(IQueryable<T> query, Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, List<string>? includes)
    {

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query;
    }
}