namespace ExtensibleCounterspam.UnitTests.MatchResultTests.ConstructorTests;

public class EmptyFilter : IFilter
{
    public static EmptyFilter Instance = new();

    private EmptyFilter()
    {
    }

    int IFilter.Count(string text)
    {
        return 0;
    }

    bool IFilter.IsMatch(string text)
    {
        return false;
    }

    IEnumerable<MatchResult> IFilter.Matches(string text)
    {
        return Enumerable.Empty<MatchResult>();
    }
}
