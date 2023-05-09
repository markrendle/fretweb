namespace FretWeb.Music.NoteTypes;

public abstract class Note : IEquatable<Note>
{
    public abstract char Letter { get; }
    public abstract Sign Sign { get; }
    public abstract string Display { get; }
    public abstract Note Alt { get; }
    
    public bool IsSharp => Sign == Sign.Sharp;
    public bool IsFlat => Sign == Sign.Flat;
    public bool IsNatural => Sign == Sign.Natural;

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
}