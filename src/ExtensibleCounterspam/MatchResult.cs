using System.ComponentModel;

namespace ExtensibleCounterspam;

/// <summary>
/// Represents a <see cref="IFilter"/> match.
/// </summary>
public sealed record MatchResult
{
    /// <summary>
    /// Gets the filter matched against.
    /// </summary>
    public IFilter Filter { get; }

    /// <summary>
    /// Gets the text matched against.
    /// </summary>
    public string OriginalText { get; }

    /// <summary>
    /// Gets the match's start index(inclusive).
    /// </summary>
    public int StartIndex { get; }

    /// <summary>
    /// Gets the match's end index(exclusive).
    /// </summary>
    public int EndIndex { get; }

    /// <summary>
    /// Gets the match's length.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// A read-only representation of the matched portion.
    /// </summary>
    public ReadOnlySpan<char> Span => OriginalText.AsSpan(StartIndex, Length);

    /// <summary>
    /// Initializes a new instance of <see cref="MatchResult"/> with the specified properties. <b>DOES NOT CHECK PARAMETERS.</b>
    /// </summary>
    /// <param name="filter">The filter matched against.</param>
    /// <param name="originalText">The text matched against.</param>
    /// <param name="startIndex">The match's start index(inclusive).</param>
    /// <param name="endIndex">The match's end index(exclusive).</param>
    /// <param name="length">The match's length.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public MatchResult(
        IFilter filter,
        int startIndex,
        int endIndex,
        string originalText,
        int length
    )
    {
        Filter = filter;
        OriginalText = originalText;
        StartIndex = startIndex;
        EndIndex = endIndex;
        Length = length;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MatchResult"/> for <paramref name="filter"/> and <paramref name="originalText"/> spanning its entirety.
    /// </summary>
    /// <param name="filter">The filter matched against.</param>
    /// <param name="originalText">The text matched against.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="filter"/> is null.
    /// </exception>
    public MatchResult(
        IFilter filter,
        string originalText
    )
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        OriginalText = originalText ?? throw new ArgumentNullException(nameof(originalText));
        StartIndex = 0;
        EndIndex = originalText.Length;
        Length = originalText.Length;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MatchResult"/> for <paramref name="filter"/> and <paramref name="originalText"/> starting at <paramref name="startIndex"/> and spanning to the end.
    /// </summary>
    /// <param name="filter">The filter matched against.</param>
    /// <param name="originalText">The text matched against.</param>
    /// <param name="startIndex">The match's start index(inclusive).</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="filter"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="startIndex"/> is less than 0 or indicates a position not within <paramref name="originalText"/>.
    /// </exception>
    public MatchResult(
        IFilter filter,
        string originalText,
        int startIndex
    )
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        OriginalText = originalText ?? throw new ArgumentNullException(nameof(originalText));
        StartIndex = startIndex >= 0 && startIndex <= originalText.Length
            ? startIndex
            : throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, null)
            ;
        EndIndex = originalText.Length;
        Length = originalText.Length - startIndex;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MatchResult"/> for <paramref name="filter"/> and <paramref name="originalText"/> starting at <paramref name="startIndex"/> and taking <paramref name="length"/> characters or ending at <paramref name="length"/> added to <paramref name="startIndex"/>.
    /// </summary>
    /// <param name="filter">The filter matched against.</param>
    /// <param name="originalText">The text matched against.</param>
    /// <param name="startIndex">The match's start index(inclusive).</param>
    /// <param name="length">The match's length.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="filter"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="startIndex"/> indicates a position not within <paramref name="originalText"/>.
    /// <paramref name="startIndex"/> added to <paramref name="length"/> indicates a position not within <paramref name="originalText"/>.
    /// <paramref name="startIndex"/> or <paramref name="length"/> is less than zero.
    /// </exception>
    public MatchResult(
        IFilter filter,
        string originalText,
        int startIndex,
        int length
    )
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        OriginalText = originalText ?? throw new ArgumentNullException(nameof(originalText));
        StartIndex = startIndex >= 0 && startIndex <= originalText.Length
            ? startIndex
            : throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, null)
            ;
        var endIndex = startIndex + length;
        Length = length >= 0 && endIndex <= originalText.Length
            ? length
            : throw new ArgumentOutOfRangeException(nameof(length), length, null)
            ;
        EndIndex = endIndex;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="MatchResult"/> for <paramref name="filter"/> and <paramref name="originalText"/> starting at <paramref name="startIndex"/> and ending at <paramref name="endIndex"/>.
    /// </summary>
    /// <param name="filter">The filter matched against.</param>
    /// <param name="startIndex">The match's start index(inclusive).</param>
    /// <param name="endIndex">The match's end index(exclusive).</param>
    /// <param name="originalText">The text matched against.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="filter"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="startIndex"/> or <paramref name="endIndex"/> indicates a position not within <paramref name="originalText"/>.
    /// <paramref name="startIndex"/> is bigger than <paramref name="endIndex"/>.
    /// <paramref name="startIndex"/> or <paramref name="endIndex"/> is less than zero.
    /// </exception>
    public MatchResult(
        IFilter filter,
        int startIndex,
        int endIndex,
        string originalText
    )
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        OriginalText = originalText ?? throw new ArgumentNullException(nameof(originalText));
        StartIndex = startIndex >= 0 && startIndex <= originalText.Length && startIndex <= endIndex
            ? startIndex
            : throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, null)
            ;
        EndIndex = endIndex >= 0 && endIndex <= originalText.Length
            ? endIndex
            : throw new ArgumentOutOfRangeException(nameof(endIndex), endIndex, null)
            ;
        Length = EndIndex - StartIndex;
    }

    /// <summary>
    /// Defines an explicit conversion of a <see cref="SpanMatchResult"/> to a <see cref="MatchResult"/>.
    /// </summary>
    /// <param name="match">The <see cref="SpanMatchResult"/> to convert.</param>
    public static explicit operator MatchResult(SpanMatchResult match)
    {
        return new MatchResult(
            match.Filter,
            match.StartIndex,
            match.EndIndex,
            match.OriginalText.ToString(),
            match.Length
        );
    }
}
