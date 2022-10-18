namespace ExtensibleCounterspam;

/// <summary>
/// Represents a type with a singleton instance.
/// </summary>
public interface ISingleton<TSelf>
    where TSelf : ISingleton<TSelf>
{
    /// <summary>
    /// Gets the one and only instance of <typeparamref name="TSelf"/>.
    /// </summary>
    static TSelf Instance { get; }
}
