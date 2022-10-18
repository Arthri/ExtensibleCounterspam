using System.Runtime.CompilerServices;

namespace ExtensibleCounterspam.UnitTests.MatchResultTests.ConstructorTests;

public class ExceptionsTest
{
    [Theory]
    [InlineData(null, null, -1, -1, -1)]
    [InlineData(null, null, 10, 1, 0)]
    [InlineData(null, null, 0, 0, 10)]
    public void HiddenConstructor_ShouldNotThorw(
        IFilter filter,
        string originalText,
        int startIndex,
        int endIndex,
        int length
    )
    {
        FluentActions.Invoking(() => _ = new MatchResult(filter, startIndex, endIndex, originalText, length)).Should()
            .NotThrow()
            ;
    }

    [Fact]
    public void Constructors_NullFilter_ThrowsArgumentNullException()
    {
        Action[] constructors =
        {
            static () => _ = new MatchResult(null!, ""),
            static () => _ = new MatchResult(null!, "", 0),
            static () => _ = new MatchResult(null!, "", 0, 0),
            static () => _ = new MatchResult(null!, 0, 0, ""),
        };

        for (var i = 0; i < constructors.Length; i++)
        {
            Action constructor = constructors[i];
            constructor.Should()
                .Throw<ArgumentNullException>()
                .Where(e => e.ParamName == "filter")
                ;
        }
    }

    [Fact]
    public void Constructors_NullOriginalText_ThrowsArgumentNullException()
    {
        Action[] constructors =
        {
            static () => _ = new MatchResult(EmptyFilter.Instance, null!),
            static () => _ = new MatchResult(EmptyFilter.Instance, null!, 0),
            static () => _ = new MatchResult(EmptyFilter.Instance, null!, 0, 0),
            static () => _ = new MatchResult(EmptyFilter.Instance, 0, 0, null!),
        };

        for (var i = 0; i < constructors.Length; i++)
        {
            Action constructor = constructors[i];
            constructor.Should()
                .Throw<ArgumentNullException>()
                .Where(e => e.ParamName == "originalText")
                ;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSet(int expected, object? value)
    {
        return value is int num && expected == num;
    }

    [Theory]
    [InlineData(-12)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(12)]
    public void Constructors_StartIndexOutOfRange_ThrowsArgumentOutOfRangeException(int startIndex)
    {
        Action[] constructors =
        {
            () => _ = new MatchResult(EmptyFilter.Instance, "", startIndex),
            () => _ = new MatchResult(EmptyFilter.Instance, "", startIndex, 0),
            () => _ = new MatchResult(EmptyFilter.Instance, startIndex, 0, ""),
        };

        for (var i = 0; i < constructors.Length; i++)
        {
            Action constructor = constructors[i];
            constructor.Should()
                .Throw<ArgumentOutOfRangeException>()
                .Where(e => IsSet(startIndex, e.ActualValue) && e.ParamName == nameof(startIndex))
                ;
        }
    }

    [Theory]
    [InlineData(-12)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(12)]
    public void Constructors_LengthOutOfRange_ThrowsArgumentOutOfRangeException(int length)
    {
        FluentActions.Invoking(() => _ = new MatchResult(EmptyFilter.Instance, "", 0, length)).Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(e => IsSet(length, e.ActualValue) && e.ParamName == nameof(length))
            ;
    }

    [Theory]
    [InlineData(2)]
    [InlineData(12)]
    public void Constructors_EndIndexOutOfRange_ThrowsArgumentOutOfRangeException(int endIndex)
    {
        FluentActions.Invoking(() => _ = new MatchResult(EmptyFilter.Instance, 0, endIndex, "")).Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(e => IsSet(endIndex, e.ActualValue) && e.ParamName == nameof(endIndex))
            ;
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(12, -12)]
    public void Constructors_StartIndexIsLargerThanEndIndex_ThrowsArgumentOutOfRangeException(int startIndex, int endIndex)
    {
        FluentActions.Invoking(() => _ = new MatchResult(EmptyFilter.Instance, startIndex, endIndex, "")).Should()
            .Throw<ArgumentOutOfRangeException>()
            .Where(e => IsSet(startIndex, e.ActualValue) && e.ParamName == nameof(startIndex))
            ;
    }
}
