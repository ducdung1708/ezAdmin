namespace Models.Models.Result
{
    public class PaginateResult<T1, T2>
    {
        public int TotalRecord { get; set; }
        public List<T1>? RecordsList { get; set; }
        public T2? Statistics { get; set; }
    }
}
