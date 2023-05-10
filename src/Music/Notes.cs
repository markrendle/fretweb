using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public static class Notes
{
    public static readonly Note CFlat = new CFlat();
    public static readonly Note C = new C();
    public static readonly Note CSharp = new CSharp();
    public static readonly Note DFlat = new DFlat();
    public static readonly Note D = new D();
    public static readonly Note DSharp = new DSharp();
    public static readonly Note EFlat = new EFlat();
    public static readonly Note E = new E();
    public static readonly Note ESharp = new ESharp();
    public static readonly Note FFlat = new FFlat();
    public static readonly Note F = new F();
    public static readonly Note FSharp = new FSharp();
    public static readonly Note GFlat = new GFlat();
    public static readonly Note G = new G();
    public static readonly Note GSharp = new GSharp();
    public static readonly Note AFlat = new AFlat();
    public static readonly Note A = new A();
    public static readonly Note ASharp = new ASharp();
    public static readonly Note BFlat = new BFlat();
    public static readonly Note B = new B();
    public static readonly Note BSharp = new BSharp();

    private static Dictionary<char, Note[]>? _lookup;

    public static Note Get(char letter, Sign sign)
    {
        var lookup = _lookup ??= CreateLookup();
        letter = char.ToUpperInvariant(letter);
        int index = (int)sign;
        if (index is < 0 or > 2) throw new ArgumentOutOfRangeException(nameof(sign));
        if (!lookup.TryGetValue(letter, out var notes)) throw new ArgumentOutOfRangeException(nameof(letter));
        return notes[index];
    }

    public static IEnumerable<Note> All()
    {
        yield return C;
        yield return CSharp;
        yield return DFlat;
        yield return D;
        yield return DSharp;
        yield return EFlat;
        yield return E;
        // yield return ESharp;
        // yield return FFlat;
        yield return F;
        yield return FSharp;
        yield return GFlat;
        yield return G;
        yield return GSharp;
        yield return AFlat;
        yield return A;
        yield return ASharp;
        yield return BFlat;
        yield return B;
        // yield return BSharp;
        // yield return CFlat;
    }

    private static Dictionary<char, Note[]> CreateLookup()
    {
        // Sign enum is Natural 0, Flat 1, Sharp 2
        return new Dictionary<char, Note[]>()
        {
            ['C'] = new[] { C, CFlat, CSharp },
            ['D'] = new[] { D, DFlat, DSharp },
            ['E'] = new[] { E, EFlat, ESharp },
            ['F'] = new[] { F, FFlat, FSharp },
            ['G'] = new[] { G, GFlat, GSharp },
            ['A'] = new[] { A, AFlat, ASharp },
            ['B'] = new[] { B, BFlat, BSharp },
        };
    }
}