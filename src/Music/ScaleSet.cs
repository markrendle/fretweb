using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class ScaleSet
{
    internal ScaleSet(Scale c, Scale dFlat, Scale d, Scale eFlat, Scale e, Scale f, Scale gFlat, Scale g, Scale aFlat, Scale a, Scale bFlat, Scale b)
    {
        C = c;
        DFlat = dFlat;
        D = d;
        EFlat = eFlat;
        E = e;
        F = f;
        GFlat = gFlat;
        G = g;
        AFlat = aFlat;
        A = a;
        BFlat = bFlat;
        B = b;
    }

    public Scale C { get; }
    public Scale DFlat { get; }
    public Scale D { get; }
    public Scale EFlat { get; }
    public Scale E { get; }
    public Scale F { get; }
    public Scale GFlat { get; }
    public Scale G { get; }
    public Scale AFlat { get; }
    public Scale A { get; }
    public Scale BFlat { get; }
    public Scale B { get; }

    public IEnumerable<Scale> Enumerate()
    {
        yield return C;
        yield return D;
        yield return E;
        yield return F;
        yield return G;
        yield return A;
        yield return B;
        yield return DFlat;
        yield return EFlat;
        yield return GFlat;
        yield return AFlat;
        yield return BFlat;
    }

    public Scale Get(Note note)
    {
        return note.Letter switch
        {
            'C' => C,
            'D' when note.IsFlat => DFlat,
            'D' => D,
            'E' when note.IsFlat => EFlat,
            'E' => E,
            'F' => F,
            'G' when note.IsFlat => GFlat,
            'G' => G,
            'A' when note.IsFlat => AFlat,
            'A' => A,
            'B' when note.IsFlat => BFlat,
            'B' => B,
            _ => throw new ArgumentException("No scale for that note")
        };
    }
}