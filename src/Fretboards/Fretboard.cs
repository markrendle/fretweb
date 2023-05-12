using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Fretboards;

public class Fretboard
{
    private readonly Fret[] _frets;

    public Fretboard(Fret[] frets)
    {
        _frets = frets;
    }

    public IReadOnlyList<Fret> Frets => _frets;

    public void ClearBadges()
    {
        foreach (var fret in _frets)
        {
            fret.ClearBadges();
        }
    }

    public void SetBadges(Scale scale, Sign sign)
    {
        if (sign == Sign.Natural) sign = Sign.Sharp;
        foreach (var fret in _frets)
        {
            fret.SetBadges(scale, sign);
        }
    }

    public void SetBadges(Chord chord, Note rootNote)
    {
        foreach (var fret in _frets)
        {
            fret.SetBadges(chord, rootNote);
        }
    }

    public void SetBadges(Note note)
    {
        foreach (var fret in _frets)
        {
            fret.SetBadges(note);
        }
    }

    public static Fretboard Create(int fretCount, params Note[] openNotes)
    {
        var frets = new List<Fret>();
        var strings = new List<FretString>(openNotes.Length);
        var notes = openNotes.Reverse().ToArray();
        
        for (int f = 0; f <= fretCount; f++)
        {
            strings.Clear();
            for (int n = 0; n < notes.Length; n++)
            {
                strings.Add(new FretString(notes[n]));
                notes[n] = notes[n].AddSemitone();
            }
            frets.Add(new Fret(f, strings.ToArray()));
        }

        return new Fretboard(frets.ToArray());
    }
}