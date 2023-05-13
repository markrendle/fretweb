using System.Diagnostics.CodeAnalysis;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public static class Notes
{
    public static readonly Note CFlat = NoteTypes.CFlat.Instance;
    public static readonly Note C = NoteTypes.C.Instance;
    public static readonly Note CSharp = NoteTypes.CSharp.Instance;
    public static readonly Note DFlat = NoteTypes.DFlat.Instance;
    public static readonly Note D = NoteTypes.D.Instance;
    public static readonly Note DSharp = NoteTypes.DSharp.Instance;
    public static readonly Note EFlat = NoteTypes.EFlat.Instance;
    public static readonly Note E = NoteTypes.E.Instance;
    public static readonly Note ESharp = NoteTypes.ESharp.Instance;
    public static readonly Note FFlat = NoteTypes.FFlat.Instance;
    public static readonly Note F = NoteTypes.F.Instance;
    public static readonly Note FSharp = NoteTypes.FSharp.Instance;
    public static readonly Note GFlat = NoteTypes.GFlat.Instance;
    public static readonly Note G = NoteTypes.G.Instance;
    public static readonly Note GSharp = NoteTypes.GSharp.Instance;
    public static readonly Note AFlat = NoteTypes.AFlat.Instance;
    public static readonly Note A = NoteTypes.A.Instance;
    public static readonly Note ASharp = NoteTypes.ASharp.Instance;
    public static readonly Note BFlat = NoteTypes.BFlat.Instance;
    public static readonly Note B = NoteTypes.B.Instance;
    public static readonly Note BSharp = NoteTypes.BSharp.Instance;

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

    public static bool TryGet(string id, [NotNullWhen(true)] out Note? note)
    {
        note = All().FirstOrDefault(n => n.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        return note is not null;
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

    public static IEnumerable<Note> ChromaticWithFlats()
    {
        yield return C;
        yield return DFlat;
        yield return D;
        yield return EFlat;
        yield return E;
        yield return F;
        yield return GFlat;
        yield return G;
        yield return AFlat;
        yield return A;
        yield return BFlat;
        yield return B;
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