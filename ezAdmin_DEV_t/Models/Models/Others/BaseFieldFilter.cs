namespace Models.Models.Others
{
    public class BaseFieldFilter<T>
    {
        public bool? All { get; set; }
        public List<T> In { get; set; } = new List<T>();
    }
}
