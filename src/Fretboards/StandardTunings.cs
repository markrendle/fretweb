namespace FretWeb.Fretboards;

public class StandardTuning
{
    public StandardTuning(string group, int strings, string name, string tuning, int order)
    {
        Group = group;
        Strings = strings;
        Name = name;
        Tuning = tuning;
        Order = order;
    }

    public string Group { get; }
    public int Strings { get; }
    public string Name { get; }
    public string Tuning { get; }
    public int Order { get; }
}

public static class StandardTunings
{
    private static readonly StandardTuning[] Tunings =
    {
        new("Bass", 4, "Standard", "EADG", 1),
        new("Bass", 4, "Drop-D", "DADG", 2),
        new("Bass", 4, "Semitone Down", "EfAfDfGf", 3),
        new("Bass", 4, "Tone Down", "DGCF", 4),
        new("Bass", 5, "Standard", "BEADG", 1),
        new("Bass", 5, "Drop-A", "AEADG", 2),
        new("Bass", 5, "Semitone Down", "BfEfAfDfGf", 3),
        new("Bass", 5, "Tone Down", "ADGCF", 4),
        new("Bass", 6, "Standard", "BEADGC", 1),
        new("Bass", 6, "Drop-A", "AEADGC", 2),
        new("Bass", 6, "Semitone Down", "BfEfAfDfGfB", 3),
        new("Bass", 6, "Tone Down", "ADGCFBf", 4),
        new("Guitar", 6, "Standard", "EADGBE", 1),
        new("Guitar", 6, "Drop-D", "DADGBE", 2),
        new("Guitar", 6, "Semitone Down", "EfAfDfGfBfEf", 3),
        new("Guitar", 6, "Tone Down", "DGCFAD", 4),
        new("Ukulele", 4, "Standard", "GCEA", 1),
        new("Ukulele", 4, "D-Tuning", "ADFsB", 2),
        new("Ukulele", 4, "Slack-Key", "GCEG", 3),
        new("Ukulele", 4, "Slide", "GCEBf", 4),
        new("Ukulele", 4, "Baritone", "DGBE", 5),
    };

    private static HashSet<string>? _tuningSet;

    public static IEnumerable<StandardTuning> All() => Tunings.AsEnumerable();

    public static IEnumerable<StandardTuning> Standard() => Tunings.Where(t => t.Name == "Standard");

    public static IEnumerable<StandardTuning> Dropdown() => Tunings.Where(t => t is { Group: "Bass", Strings: 4 }).OrderBy(t => t.Order)
        .Concat(Tunings.Where(t => t is { Group: "Guitar", Strings: 6 }).OrderBy(t => t.Order))
        .Concat(Tunings.Where(t => t is { Group: "Ukulele", Strings: 4 }).OrderBy(t => t.Order));

    public static IEnumerable<StandardTuning> Group(string group) => Tunings.Where(t => t.Group.Equals(group, StringComparison.OrdinalIgnoreCase));

    public static bool IsStandardTuning(string tuning)
    {
        _tuningSet ??= Tunings.Select(t => t.Tuning).ToHashSet(StringComparer.OrdinalIgnoreCase);
        return _tuningSet.Contains(tuning);
    }
}
