using System.Diagnostics.CodeAnalysis;

namespace FretWeb.Music;

public static class Arpeggios
{
    private static Arpeggio[]? _arpeggios;

    private static Arpeggio[] Array => _arpeggios ??= ArpeggioParser.Load().ToArray();

    public static bool TryGet(string id, [NotNullWhen(true)] out Arpeggio? arpeggio)
    {
        arpeggio = All().FirstOrDefault(c => c.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        return arpeggio is not null;
    }

    public static IEnumerable<Arpeggio> All() => Array.AsEnumerable();

    public static IEnumerable<Arpeggio> AllInGroup(string group) => Array.Where(a => a.Group.Equals(group, StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<Arpeggio> AllMajor() => AllInGroup("Major");

    public static IEnumerable<Arpeggio> AllMinor() => AllInGroup("Minor");

    public static IEnumerable<Arpeggio> AllDominant() => AllInGroup("Dominant");

    public static IEnumerable<Arpeggio> AllSus() => AllInGroup("Sus");

    public static IEnumerable<Arpeggio> AllDiminished() => AllInGroup("Diminished");

    public static IEnumerable<Arpeggio> AllAugmented() => AllInGroup("Augmented");
}