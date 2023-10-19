using System.Linq.Expressions;

namespace Models.Models.ParamsFunction
{
    public class ParamsFilter<T>
    {
        public Expression<Func<T, bool>> Predicate { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
