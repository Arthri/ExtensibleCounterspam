namespace ExtensibleCounterspam.UnitTests.MatchResultTests.ConstructorTests;

public partial class CorrectlyAssignsPropertiesTest
{
    [Theory]
    [InlineData("12345", 1, 2, 3)]
    public void HiddenConstructor(string matchedString, int startIndex, int endIndex, int length)
    {
        var matchResult = new MatchResult(
            EmptyFilter.Instance,
            startIndex,
            endIndex,
            matchedString,
            length
        );

        matchResult.Filter.Should()
            .BeSameAs(EmptyFilter.Instance)
            ;
        matchResult.OriginalText.Should()
            .Be(matchedString)
            ;
        matchResult.StartIndex.Should()
            .Be(startIndex)
            ;
        matchResult.EndIndex.Should()
            .Be(endIndex)
            ;
        matchResult.Length.Should()
            .Be(length)
            ;
    }

    [Theory]
    [InlineData("12345", 5, 5)]
    public void Constructor_Basic(string matchedString, int expectedEndIndex, int expectedLength)
    {
        var matchResult = new MatchResult(
            EmptyFilter.Instance,
            matchedString
        );

        matchResult.Filter.Should()
            .BeSameAs(EmptyFilter.Instance)
            ;
        matchResult.OriginalText.Should()
            .Be(matchedString)
            ;
        matchResult.StartIndex.Should()
            .Be(0)
            ;
        matchResult.EndIndex.Should()
            .Be(expectedEndIndex)
            ;
        matchResult.Length.Should()
            .Be(expectedLength)
            ;
        matchedString.AsSpan()
            .Equals(matchResult.Span, StringComparison.Ordinal).Should()
            .BeTrue()
            ;
    }

    [Theory]
    [InlineData("12345", 1, 5, 4)]
    [InlineData("12345", 5, 5, 0)]
    public void Constructor_StartIndexOnly(string matchedString, int startIndex, int expectedEndIndex, int expectedLength)
    {
        var matchResult = new MatchResult(
            EmptyFilter.Instance,
            matchedString,
            startIndex
        );

        matchResult.Filter.Should()
            .BeSameAs(EmptyFilter.Instance)
            ;
        matchResult.OriginalText.Should()
            .Be(matchedString)
            ;
        matchResult.StartIndex.Should()
            .Be(startIndex)
            ;
        matchResult.EndIndex.Should()
            .Be(expectedEndIndex)
            ;
        matchResult.Length.Should()
            .Be(expectedLength)
            ;
        matchedString.AsSpan(startIndex)
            .Equals(matchResult.Span, StringComparison.Ordinal).Should()
            .BeTrue()
            ;
    }

    [Theory]
    [InlineData("12345", 1, 4, 3)]
    [InlineData("12345", 1, 5, 4)]
    public void Constructor_StartIndexAndLength(string matchedString, int startIndex, int expectedEndIndex, int length)
    {
        var matchResult = new MatchResult(
            EmptyFilter.Instance,
            matchedString,
            startIndex,
            length
        );

        matchResult.Filter.Should()
            .BeSameAs(EmptyFilter.Instance)
            ;
        matchResult.OriginalText.Should()
            .Be(matchedString)
            ;
        matchResult.StartIndex.Should()
            .Be(startIndex)
            ;
        matchResult.EndIndex.Should()
            .Be(expectedEndIndex)
            ;
        matchResult.Length.Should()
            .Be(length)
            ;
        matchedString.AsSpan(startIndex, length)
            .Equals(matchResult.Span, StringComparison.Ordinal).Should()
            .BeTrue()
            ;
    }

    [Theory]
    [InlineData("12345", 0, 0, 0)]
    [InlineData("12345", 1, 3, 2)]
    [InlineData("12345", 1, 5, 4)]
    public void Constructor_StartIndexAndEndIndex(string matchedString, int startIndex, int endIndex, int expectedLength)
    {
        var matchResult = new MatchResult(
            EmptyFilter.Instance,
            startIndex,
            endIndex,
            matchedString
        );

        matchResult.Filter.Should()
            .BeSameAs(EmptyFilter.Instance)
            ;
        matchResult.OriginalText.Should()
            .Be(matchedString)
            ;
        matchResult.StartIndex.Should()
            .Be(startIndex)
            ;
        matchResult.EndIndex.Should()
            .Be(endIndex)
            ;
        matchResult.Length.Should()
            .Be(expectedLength)
            ;
        matchedString.AsSpan(startIndex, endIndex - startIndex)
            .Equals(matchResult.Span, StringComparison.Ordinal).Should()
            .BeTrue()
            ;
    }
}
