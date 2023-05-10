using System.Diagnostics.CodeAnalysis;

namespace FretWeb.Music.NoteTypes;

public abstract class Note : IEquatable<Note>
{
    private string? _text;
    
    public abstract int Value { get; }
    public abstract char Letter { get; }
    public abstract Sign Sign { get; }
    public abstract string Display { get; }
    public abstract Note Alt { get; }

    public string Text => _text ??= GetText();

    public bool IsSharp => Sign == Sign.Sharp;
    public bool IsFlat => Sign == Sign.Flat;
    public bool IsNatural => Sign == Sign.Natural;
    public virtual bool IsTheoretical => false;

    public abstract Note AddSemitone();
    public Note AddTone() => AddSemitone().AddSemitone();
    public abstract Note SubtractSemitone();
    public Note SubtractTone() => SubtractSemitone().SubtractSemitone();

    public bool IsEquivalentTo(Note other) => Equals(other) || Equals(other.Alt);

    public override bool Equals(object? obj) => obj is Note note && Equals(note);

    public bool Equals(Note? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Letter == other.Letter && Sign == other.Sign;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Letter, (int)Sign);
    }

    public static bool operator ==(Note? left, Note? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Note? left, Note? right)
    {
        return !Equals(left, right);
    }

    private string GetText()
    {
        var sign = Sign switch
        {
            Sign.Natural => "",
            Sign.Flat => "-flat",
            Sign.Sharp => "-sharp",
            _ => throw new ArgumentOutOfRangeException()
        };
        return $"{Letter}{sign}";
    }

    public static bool TryParse(string str, [NotNullWhen(true)] out Note? note)
    {
        if (str is not { Length: > 0 }) goto fail;

        char c = char.ToUpperInvariant(str[0]);
        if (c is < 'A' or > 'G') goto fail;

        if (str.Length == 1)
        {
            note = GetNatural(c);
            return true;
        }

        var rest = str.AsSpan().Slice(1).Trim().TrimStart('-').ToString();

        if (rest.Equals("flat", StringComparison.OrdinalIgnoreCase) || rest.Equals(DisplayStrings.Flat))
        {
            note = GetFlat(c);
            return true;
        }

        if (rest.Equals("sharp", StringComparison.OrdinalIgnoreCase) || rest.Equals(DisplayStrings.Sharp))
        {
            note = GetSharp(c);
            return true;
        }

        fail:
        note = null;
        return false;
    }

    public static Note Get(char letter, Sign sign) =>
        sign switch
        {
            Sign.Natural => GetNatural(letter),
            Sign.Flat => GetFlat(letter),
            Sign.Sharp => GetSharp(letter),
            _ => throw new ArgumentOutOfRangeException(nameof(sign), sign, null)
        };

    private static Note GetNatural(char letter) =>
        letter switch
        {
            'A' => Notes.A,
            'B' => Notes.B,
            'C' => Notes.C,
            'D' => Notes.D,
            'E' => Notes.E,
            'F' => Notes.F,
            'G' => Notes.G,
            _ => throw new ArgumentOutOfRangeException()
        };

    private static Note GetFlat(char letter) =>
        letter switch
        {
            'A' => Notes.AFlat,
            'B' => Notes.BFlat,
            'C' => Notes.CFlat,
            'D' => Notes.DFlat,
            'E' => Notes.EFlat,
            'F' => Notes.FFlat,
            'G' => Notes.GFlat,
            _ => throw new ArgumentOutOfRangeException()
        };

    private static Note GetSharp(char letter) =>
        letter switch
        {
            'A' => Notes.ASharp,
            'B' => Notes.BSharp,
            'C' => Notes.CSharp,
            'D' => Notes.DSharp,
            'E' => Notes.ESharp,
            'F' => Notes.FSharp,
            'G' => Notes.GSharp,
            _ => throw new ArgumentOutOfRangeException()
        };
}