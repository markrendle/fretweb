using System.Diagnostics.CodeAnalysis;

namespace FretWeb.Music;

public static class Chords
{
    public static Chord Major { get; } = new("Major", "Major", 1, 3, 5);
    public static Chord Major6 { get; } = new("Major", "Major 6", 1, 3, 5, 6);
    public static Chord Major7 { get; } = new("Major", "Major 7 ", 1, 3, 5, 7);
    public static Chord Major7Flat5 { get; } = new("Major", $"Major 7 ({DisplayStrings.Flat}5)", 1, 3, Flat(5), 7);
    public static Chord Major9 { get; } = new("Major", "Major 9 ", 1, 3, 5, 7, 9);
    public static Chord Major11 { get; } = new("Major", "Major 11", 1, 3, 5, 7, 9, 11);
    public static Chord Major13 { get; } = new("Major", "Major 13", 1, 3, 5, 7, 9, 11, 13);

    public static Chord Minor { get; } = new("Minor", "Minor", 1, Flat(3), 5);
    public static Chord Minor6 { get; } = new("Minor", "Minor 6", 1, Flat(3), 5, 6);
    public static Chord Minor7 { get; } = new("Minor", "Minor 7", 1, Flat(3), 5, Flat(7));
    public static Chord Minor7Flat5 { get; } = new("Minor", $"Minor 7({DisplayStrings.Flat}5)", 1, Flat(3), Flat(5), Flat(7));
    public static Chord MinorMajor7 { get; } = new("Minor", "Minor 7", 1, Flat(3), 5, 7);
    public static Chord Minor9 { get; } = new("Minor", "Minor 9", 1, Flat(3), 5, Flat(7), 9);
    public static Chord Minor11 { get; } = new("Minor", "Minor 11", 1, Flat(3), 5, Flat(7), 9, 11);

    public static Chord Dominant7 { get; } = new("Dominant", "Dominant 7", 1, 3, 5, Flat(7));
    public static Chord Dominant7Flat5 { get; } = new("Dominant", $"Dominant 7({DisplayStrings.Flat}5)", 1, 3, Flat(5), Flat(7));
    public static Chord Dominant9 { get; } = new("Dominant", "Dominant 9", 1, 3, 5, Flat(7), 9);
    public static Chord Dominant11 { get; } = new("Dominant", "Dominant 11", 1, 3, 5, Flat(7), 9, 11);
    public static Chord Dominant13 { get; } = new("Dominant", "Dominant 13", 1, 3, 5, Flat(7), 9, 11, 13);
    public static Chord Dominant7Sus4 { get; } = new("Dominant", "Dominant 7 Sus 4", 1, 4, 5, Flat(7));

    public static Chord Sus2 { get; } = new("Sus", "Sus 2", 1, 2, 5);
    public static Chord Sus4 { get; } = new("Sus", "Sus 4", 1, 4, 5);

    public static Chord Diminished { get; } = new("Diminished", "Diminished", 1, Flat(3), Flat(5));
    public static Chord Diminished7 { get; } = new("Diminished", "Diminished 7", 1, Flat(3), Flat(5), FlatFlat(7));
    public static Chord HalfDiminished7 { get; } = new("Diminished", "Half Diminished 7", 1, Flat(3), Flat(5), Flat(7));

    public static Chord Augmented { get; } = new("Augmented", "Augmented", 1, 3, Sharp(5));
    public static Chord Augmented7 { get; } = new("Augmented", "Augmented 7", 1, 3, Sharp(5), Flat(7));

    public static bool TryGet(string id, [NotNullWhen(true)] out Chord? chord)
    {
        chord = All().FirstOrDefault(c => c.Id == id);
        return chord is not null;
    }

    public static IEnumerable<Chord> All() =>
        AllMajor()
            .Concat(AllMinor())
            .Concat(AllDominant())
            .Concat(AllSus())
            .Concat(AllDiminished())
            .Concat(AllAugmented());

    public static IEnumerable<Chord> AllMajor()
    {
        yield return Major;
        yield return Major6;
        yield return Major7;
        yield return Major7Flat5;
        yield return Major9;
        yield return Major11;
        yield return Major13;
    }

    public static IEnumerable<Chord> AllMinor()
    {
        yield return Minor;
        yield return Minor6;
        yield return Minor7;
        yield return Minor7Flat5;
        yield return MinorMajor7;
        yield return Minor9;
        yield return Minor11;
    }

    public static IEnumerable<Chord> AllDominant()
    {
        yield return Dominant7;
        yield return Dominant7Flat5;
        yield return Dominant9;
        yield return Dominant11;
        yield return Dominant13;
        yield return Dominant7Sus4;
    }

    public static IEnumerable<Chord> AllSus()
    {
        yield return Sus2;
        yield return Sus4;
    }

    public static IEnumerable<Chord> AllDiminished()
    {
        yield return Diminished;
        yield return Diminished7;
        yield return HalfDiminished7;
    }

    public static IEnumerable<Chord> AllAugmented()
    {
        yield return Augmented;
        yield return Augmented7;
    }

    private static ChordNote Flat(int number) => new(number, Sign.Flat);
    private static ChordNote FlatFlat(int number) => new(number, Sign.FlatFlat);
    private static ChordNote Sharp(int number) => new(number, Sign.Sharp);
}