namespace PathFinder
{
    public interface IBetterComparable<T>
    {
        int CompareTo(T obj, Enums.ComparisonType compType = Enums.ComparisonType.Default);
    }
}
