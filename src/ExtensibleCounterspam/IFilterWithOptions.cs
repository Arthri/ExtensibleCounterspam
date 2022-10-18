namespace ExtensibleCounterspam;

/// <summary>
/// Represents a filter that accepts options.
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public interface IFilterWithOptions<TOptions> : IFilter
{
    /// <summary>
    /// Gets the filter's configuration.
    /// </summary>
    TOptions Options { get; }
}
