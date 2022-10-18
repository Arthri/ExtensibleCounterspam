namespace ExtensibleCounterspam;

/// <summary>
/// Represents an object that filters <see langword="string"/>s.
/// </summary>
public interface IFilter
{
    /// <summary>
    /// Counts the number of filter matches against the specified <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to match against.</param>
    /// <returns>The number of matches.</returns>
    int Count(string text);

    /// <summary>
    /// Determines if the filter matches the specified <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to match against.</param>
    /// <returns><see langword="true"/> if the filter matches the specified <paramref name="text"/>; otherwise, <see langword="false"/>.</returns>
    bool IsMatch(string text);

    /// <summary>
    /// Finds all filter matches against the specified <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to match against.</param>
    /// <returns>All filter matches found.</returns>
    IEnumerable<MatchResult> Matches(string text);
}
