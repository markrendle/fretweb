namespace FretWeb.Fretboards;

public class StandardTuning
{
    public StandardTuning(string group, string name, string tuning)
    {
        Group = group;
        Name = name;
        Tuning = tuning;
        
    }

    public string Group { get; }
    public string Name { get; }
    public string Tuning { get; }
}

public static class StandardTunings
{
    private static readonly StandardTuning[] _tunings =
    {
        new("Bass", "Standard", "EADG"),
        new("Bass", "Drop-D", "DADG"),
        new("Bass", "Semitone Down", "EfAfDfGf"),
        new("Bass", "Tone Down", "DGCF"),
        new("Guitar", "Standard", "EADGBE"),
        new("Guitar", "Drop-D", "DADGBE"),
        new("Guitar", "Semitone Down", "EfAfDfGfBfEf"),
        new("Guitar", "Tone Down", "DGCFAD"),
        new("Ukulele", "Standard", "GCEA"),
        new("Ukulele", "D-Tuning", "ADFsB"),
        new("Ukulele", "Slack-Key", "GCEG"),
        new("Ukulele", "Slide", "GCEBf"),
        new("Ukulele", "Baritone", "DGBE"),
    };

    public static IEnumerable<StandardTuning> All() => _tunings.AsEnumerable();

    public static IEnumerable<StandardTuning> Group(string group) => _tunings.Where(t => t.Group.Equals(group, StringComparison.OrdinalIgnoreCase));
}
