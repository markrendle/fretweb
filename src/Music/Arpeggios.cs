using System.Diagnostics.CodeAnalysis;

namespace FretWeb.Music;

public static class Arpeggios
{
    public static Arpeggio Major { get; } = new("Major", "Major", 1, 3, 5);
    public static Arpeggio Major6 { get; } = new("Major", "Major 6", 1, 3, 5, 6);
    public static Arpeggio Major7 { get; } = new("Major", "Major 7 ", 1, 3, 5, 7);
    public static Arpeggio Major7Flat5 { get; } = new("Major", $"Major 7 ({DisplayStrings.Flat}5)", 1, 3, Flat(5), 7);
    public static Arpeggio Major9 { get; } = new("Major", "Major 9 ", 1, 3, 5, 7, 9);
    public static Arpeggio Major11 { get; } = new("Major", "Major 11", 1, 3, 5, 7, 9, 11);
    public static Arpeggio Major13 { get; } = new("Major", "Major 13", 1, 3, 5, 7, 9, 11, 13);

    public static Arpeggio Minor { get; } = new("Minor", "Minor", 1, Flat(3), 5);
    public static Arpeggio Minor6 { get; } = new("Minor", "Minor 6", 1, Flat(3), 5, 6);
    public static Arpeggio Minor7 { get; } = new("Minor", "Minor 7", 1, Flat(3), 5, Flat(7));
    public static Arpeggio Minor7Flat5 { get; } = new("Minor", $"Minor 7({DisplayStrings.Flat}5)", 1, Flat(3), Flat(5), Flat(7));
    public static Arpeggio MinorMajor7 { get; } = new("Minor", "Minor 7", 1, Flat(3), 5, 7);
    public static Arpeggio Minor9 { get; } = new("Minor", "Minor 9", 1, Flat(3), 5, Flat(7), 9);
    public static Arpeggio Minor11 { get; } = new("Minor", "Minor 11", 1, Flat(3), 5, Flat(7), 9, 11);

    public static Arpeggio Dominant7 { get; } = new("Dominant", "Dominant 7", 1, 3, 5, Flat(7));
    public static Arpeggio Dominant7Flat5 { get; } = new("Dominant", $"Dominant 7({DisplayStrings.Flat}5)", 1, 3, Flat(5), Flat(7));
    public static Arpeggio Dominant9 { get; } = new("Dominant", "Dominant 9", 1, 3, 5, Flat(7), 9);
    public static Arpeggio Dominant11 { get; } = new("Dominant", "Dominant 11", 1, 3, 5, Flat(7), 9, 11);
    public static Arpeggio Dominant13 { get; } = new("Dominant", "Dominant 13", 1, 3, 5, Flat(7), 9, 11, 13);
    public static Arpeggio Dominant7Sus4 { get; } = new("Dominant", "Dominant 7 Sus 4", 1, 4, 5, Flat(7));

    public static Arpeggio Sus2 { get; } = new("Sus", "Sus 2", 1, 2, 5);
    public static Arpeggio Sus4 { get; } = new("Sus", "Sus 4", 1, 4, 5);

    public static Arpeggio Diminished { get; } = new("Diminished", "Diminished", 1, Flat(3), Flat(5));
    public static Arpeggio Diminished7 { get; } = new("Diminished", "Diminished 7", 1, Flat(3), Flat(5), FlatFlat(7));
    public static Arpeggio HalfDiminished7 { get; } = new("Diminished", "Half Diminished 7", 1, Flat(3), Flat(5), Flat(7));

    public static Arpeggio Augmented { get; } = new("Augmented", "Augmented", 1, 3, Sharp(5));
    public static Arpeggio Augmented7 { get; } = new("Augmented", "Augmented 7", 1, 3, Sharp(5), Flat(7));

    public static bool TryGet(string id, [NotNullWhen(true)] out Arpeggio? chord)
    {
        chord = All().FirstOrDefault(c => c.Id == id);
        return chord is not null;
    }

    public static IEnumerable<Arpeggio> All() =>
        AllMajor()
            .Concat(AllMinor())
            .Concat(AllDominant())
            .Concat(AllSus())
            .Concat(AllDiminished())
            .Concat(AllAugmented());

    public static IEnumerable<Arpeggio> AllMajor()
    {
        yield return Major;
        yield return Major6;
        yield return Major7;
        yield return Major7Flat5;
        yield return Major9;
        yield return Major11;
        yield return Major13;
    }

    public static IEnumerable<Arpeggio> AllMinor()
    {
        yield return Minor;
        yield return Minor6;
        yield return Minor7;
        yield return Minor7Flat5;
        yield return MinorMajor7;
        yield return Minor9;
        yield return Minor11;
    }

    public static IEnumerable<Arpeggio> AllDominant()
    {
        yield return Dominant7;
        yield return Dominant7Flat5;
        yield return Dominant9;
        yield return Dominant11;
        yield return Dominant13;
        yield return Dominant7Sus4;
    }

    public static IEnumerable<Arpeggio> AllSus()
    {
        yield return Sus2;
        yield return Sus4;
    }

    public static IEnumerable<Arpeggio> AllDiminished()
    {
        yield return Diminished;
        yield return Diminished7;
        yield return HalfDiminished7;
    }

    public static IEnumerable<Arpeggio> AllAugmented()
    {
        yield return Augmented;
        yield return Augmented7;
    }

    private static ArpeggioNote Flat(int number) => new(number, Sign.Flat);
    private static ArpeggioNote FlatFlat(int number) => new(number, Sign.FlatFlat);
    private static ArpeggioNote Sharp(int number) => new(number, Sign.Sharp);
}