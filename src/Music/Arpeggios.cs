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

    public static Arpeggio Major7th = Array.Single(a => a.Group == "Major" && a.Name == "Major 7th");
    public static Arpeggio Minor7th = Array.Single(a => a.Group == "Minor" && a.Name == "Minor 7th");
    public static Arpeggio Dominant7th = Array.Single(a => a.Group == "Dominant" && a.Name == "Dominant 7th");
    public static Arpeggio Diminished7th = Array.Single(a => a.Group == "Diminished" && a.Name == "Diminished 7th");
}