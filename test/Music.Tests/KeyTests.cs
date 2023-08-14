using FretWeb.Music.NoteTypes;

namespace FretWeb.Music.Tests;

public class KeyTests
{
    [Fact]
    public void GetsCMajor()
    {
        var actual = Keys.Major.Get(Notes.C);
        Assert.NotNull(actual);
        
        AssertChord(actual.Chords[0], Notes.C, KeyChordType.Major, KeyChordMode.Ionian, Arpeggios.Major7th, Scales.Ionian);
        AssertChord(actual.Chords[1], Notes.D, KeyChordType.Minor, KeyChordMode.Dorian, Arpeggios.Minor7th, Scales.Dorian);
        AssertChord(actual.Chords[2], Notes.E, KeyChordType.Minor, KeyChordMode.Phrygian, Arpeggios.Minor7th, Scales.Phrygian);
        AssertChord(actual.Chords[3], Notes.F, KeyChordType.Major, KeyChordMode.Lydian, Arpeggios.Major7th, Scales.Lydian);
        AssertChord(actual.Chords[4], Notes.G, KeyChordType.Dominant, KeyChordMode.Mixolydian, Arpeggios.Dominant7th, Scales.Mixolydian);
        AssertChord(actual.Chords[5], Notes.A, KeyChordType.Minor, KeyChordMode.Aeolian, Arpeggios.Minor7th, Scales.Aeolian);
        AssertChord(actual.Chords[6], Notes.B, KeyChordType.Diminished, KeyChordMode.Locrian, Arpeggios.Diminished7th, Scales.Locrian);
    }

    [Theory]
    [MemberData(nameof(AllTheNotes))]
    public void Major(Note root)
    {
        var notes = Scales.Major.Get(root);
        
        var actual = Keys.Major.Get(root);
        Assert.NotNull(actual);
        
        AssertChord(actual.Chords[0], notes[0], KeyChordType.Major, KeyChordMode.Ionian, Arpeggios.Major7th, Scales.Ionian);
        AssertChord(actual.Chords[1], notes[1], KeyChordType.Minor, KeyChordMode.Dorian, Arpeggios.Minor7th, Scales.Dorian);
        AssertChord(actual.Chords[2], notes[2], KeyChordType.Minor, KeyChordMode.Phrygian, Arpeggios.Minor7th, Scales.Phrygian);
        AssertChord(actual.Chords[3], notes[3], KeyChordType.Major, KeyChordMode.Lydian, Arpeggios.Major7th, Scales.Lydian);
        AssertChord(actual.Chords[4], notes[4], KeyChordType.Dominant, KeyChordMode.Mixolydian, Arpeggios.Dominant7th, Scales.Mixolydian);
        AssertChord(actual.Chords[5], notes[5], KeyChordType.Minor, KeyChordMode.Aeolian, Arpeggios.Minor7th, Scales.Aeolian);
        AssertChord(actual.Chords[6], notes[6], KeyChordType.Diminished, KeyChordMode.Locrian, Arpeggios.Diminished7th, Scales.Locrian);
    }

    [Fact]
    public void GetsCMinor()
    {
        var actual = Keys.Minor.Get(Notes.C);
        Assert.NotNull(actual);
        
        AssertChord(actual.Chords[0], Notes.C, KeyChordType.Minor, KeyChordMode.Aeolian, Arpeggios.Minor7th, Scales.Aeolian);
        AssertChord(actual.Chords[1], Notes.D, KeyChordType.Diminished, KeyChordMode.Locrian, Arpeggios.Diminished7th, Scales.Locrian);
        AssertChord(actual.Chords[2], Notes.EFlat, KeyChordType.Major, KeyChordMode.Ionian, Arpeggios.Major7th, Scales.Ionian);
        AssertChord(actual.Chords[3], Notes.F, KeyChordType.Minor, KeyChordMode.Dorian, Arpeggios.Minor7th, Scales.Dorian);
        AssertChord(actual.Chords[4], Notes.G, KeyChordType.Minor, KeyChordMode.Phrygian, Arpeggios.Minor7th, Scales.Phrygian);
        AssertChord(actual.Chords[5], Notes.AFlat, KeyChordType.Major, KeyChordMode.Lydian, Arpeggios.Major7th, Scales.Lydian);
        AssertChord(actual.Chords[6], Notes.BFlat, KeyChordType.Dominant, KeyChordMode.Mixolydian, Arpeggios.Dominant7th, Scales.Mixolydian);
    }

    [Theory]
    [MemberData(nameof(AllTheNotes))]
    public void Minor(Note root)
    {
        var notes = Scales.Minor.Get(root);
        
        var actual = Keys.Minor.Get(root);
        Assert.NotNull(actual);
        
        AssertChord(actual.Chords[0], notes[0], KeyChordType.Minor, KeyChordMode.Aeolian, Arpeggios.Minor7th, Scales.Aeolian);
        AssertChord(actual.Chords[1], notes[1], KeyChordType.Diminished, KeyChordMode.Locrian, Arpeggios.Diminished7th, Scales.Locrian);
        AssertChord(actual.Chords[2], notes[2], KeyChordType.Major, KeyChordMode.Ionian, Arpeggios.Major7th, Scales.Ionian);
        AssertChord(actual.Chords[3], notes[3], KeyChordType.Minor, KeyChordMode.Dorian, Arpeggios.Minor7th, Scales.Dorian);
        AssertChord(actual.Chords[4], notes[4], KeyChordType.Minor, KeyChordMode.Phrygian, Arpeggios.Minor7th, Scales.Phrygian);
        AssertChord(actual.Chords[5], notes[5], KeyChordType.Major, KeyChordMode.Lydian, Arpeggios.Major7th, Scales.Lydian);
        AssertChord(actual.Chords[6], notes[6], KeyChordType.Dominant, KeyChordMode.Mixolydian, Arpeggios.Dominant7th, Scales.Mixolydian);
    }

    public static IEnumerable<object[]> AllTheNotes()
    {
        yield return new object[] { Notes.C };
        yield return new object[] { Notes.DFlat };
        yield return new object[] { Notes.D };
        yield return new object[] { Notes.EFlat };
        yield return new object[] { Notes.E };
        yield return new object[] { Notes.F };
        yield return new object[] { Notes.GFlat };
        yield return new object[] { Notes.G };
        yield return new object[] { Notes.AFlat };
        yield return new object[] { Notes.A };
        yield return new object[] { Notes.BFlat };
        yield return new object[] { Notes.B };
    }

    private void AssertChord(KeyChord actual, Note root, KeyChordType type, KeyChordMode mode, Arpeggio arpeggio, ScaleSet scaleSet)
    {
        Assert.Equal(type, actual.ChordType);
        Assert.Equal(mode, actual.ChordMode);

        var expectedArpeggio = arpeggio.GetNotes(root);
        Assert.Equal(expectedArpeggio, actual.Arpeggio);

        var expectedMode = scaleSet.Get(root).ToArray();
        Assert.Equal(expectedMode, actual.Mode.ToArray());
    }
}