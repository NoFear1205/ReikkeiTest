using System.Linq.Expressions;
using Test.Domain.Helpers.GenericTypes;

namespace Test.Domain.Extensions
{
    public static class DataExtension
    {
        public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, secondBody), expr1.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, secondBody), expr1.Parameters);
        }

        private static Expression Replace(this Expression expression,
        Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        public static List<T> Paging<T>(this IQueryable<T> data, int pageSize = 10, int page = 1)
        {
            if (pageSize == 0) return data.ToList();
            return data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static ListDataSource<T> ToGridData<T>(this IQueryable<T> data, Func<IQueryable<T>, IQueryable<T>> expr, int pageSize = 10, int page = 1) where T : class
        {
            var result = expr(data);
            return new ListDataSource<T>(result.Paging(pageSize, page), result.Count(), pageSize, page);
        }
    }

    public class GridDataSource<T> where T : class
    {
        public GridDataSource(List<T> data, int total, int pageSize = 10, int page = 1)
        {
            Data = data;
            Total = total;
            PageSize = pageSize;
            Page = page;
        }
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }

    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
