namespace Test.Domain.Helpers.GenericTypes
{
    public class ListDataSource<T> where T : class
    {
        public ListDataSource() { }

        public ListDataSource(List<T> data, int total, int pageSize, int page, Dictionary<string, Aggregation>? aggregates = null)
        {
            Data = data;
            Total = total;
            PageSize = pageSize;
            Page = page;
            Aggregates = aggregates;
        }
        public List<T> Data { get; set; } = new List<T>();
        public int Total { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int Page { get; set; } = 1;
        public Dictionary<string, Aggregation>? Aggregates { get; set; }
    }

    public class Aggregation
    {
        public Aggregation(object sum, object average, int count)
        {
            Sum = sum;
            Average = average;
            Count = count;
        }
        public object Sum { get; private set; }
        public object Average { get; private set; }
        public int Count { get; private set; }
    }
}
