using System.Buffers;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public static class Normalizer
{
    public static Scale ForceFlat(Scale scale)
    {
        var span = scale.AsSpan();
        var notes = ForceFlat(span);
        if (span.SequenceEqual(notes)) return scale;
        return new Scale(notes);
    }

    public static Note[] ForceFlat(ReadOnlySpan<Note> span)
    {
        if (!span.Any(n => n.Sign is Sign.Sharp or Sign.SharpSharp)) return span.ToArray();
        
        var notes = new Note[span.Length];
        span.CopyTo(notes);
        
        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i].IsSharp)
            {
                notes[i] = notes[i].Alt;
            }
        }
        
        while (ContainsRepeatedLetter(notes))
        {
            bool changed = false;
            
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i].IsFlat) continue;
                char l = notes[i].Letter;
                if (notes.Any(n => n.Letter == l && n.IsFlat))
                {
                    notes[i] = notes[i].AsFlat();
                    changed = true;
                }
            }

            if (!changed) break;
        }

        return notes;
    }
    
    public static Scale ForceSharp(Scale scale)
    {
        var span = scale.AsSpan();
        var notes = ForceSharp(span);
        if (span.SequenceEqual(notes)) return scale;
        return new Scale(notes);
    }
    
    public static Note[] ForceSharp(ReadOnlySpan<Note> span)
    {
        var notes = new Note[span.Length];
        span.CopyTo(notes);
        
        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i].IsFlat)
            {
                notes[i] = notes[i].Alt;
            }
        }
        
        while (ContainsRepeatedLetter(notes))
        {
            bool changed = false;
            bool allSharp = true;
            
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i].IsSharp) continue;
                allSharp = false;
                char l = notes[i].Letter;
                if (notes.Any(n => n.Letter == l && n.IsSharp))
                {
                    notes[i] = notes[i].AsSharp();
                    changed = true;
                }
            }

            if (allSharp || !changed) break;
        }

        return notes;
    }
    

    public static bool ContainsRepeatedLetter(ReadOnlySpan<Note> notes)
    {
        Span<int> count = stackalloc int[7];
        foreach (var note in notes)
        {
            int index = note.Letter - 'A';
            count[index] += 1;
        }

        for (int i = 0; i < 7; i++)
        {
            if (count[i] > 1) return true;
        }

        return false;
    }

    public static bool ContainsSharps(ReadOnlySpan<Note> notes)
    {
        foreach (var note in notes)
        {
            if (note.IsSharp) return true;
        }

        return false;
    }

    private static void NormalizeFlat(Span<Note> notes)
    {
        int p = 0;
        for (int i = 1; i < notes.Length; i++)
        {
            if (notes[i].Letter == notes[p].Letter)
            {
                notes[i] = notes[i].AsFlat();
            }

            p = i;
        }
    }
    
    private static void NormalizeSharp(Span<Note> notes)
    {
        int p = notes.Length - 1;
        for (int i = notes.Length - 2; i > -1; i--)
        {
            if (notes[i].Letter == notes[p].Letter)
            {
                notes[i] = notes[i].AsSharp();
            }

            p = i;
        }
    }
    private static void NormalizeAscending(Span<Note> notes)
    {
        char l = notes[0].Letter;
        
        for (int i = 1; i < notes.Length; i++)
        {
            var note = notes[i];
            switch (DistanceUp(l, note.Letter))
            {
                case 0:
                    notes[i] = note.AsFlat();
                    break;
                case 2:
                    notes[i] = note.AsSharp();
                    break;
            }

            l = notes[i].Letter;
        }
    }

    private static void NormalizeDescending(Span<Note> notes)
    {
        char l = notes[^1].Letter;
        
        for (int i = notes.Length - 2; i > -1; i--)
        {
            var note = notes[i];
            switch (DistanceDown(l, note.Letter))
            {
                case 0:
                    notes[i] = note.AsFlat();
                    break;
                case 2:
                    notes[i] = note.AsSharp();
                    break;
            }

            l = notes[i].Letter;
        }
    }

    private static int DistanceUp(char from, char to)
    {
        if (from == to) return 0;
        
        int f = from - 'A';
        int t = to - 'A';
        if (t < f) t += 7;
        return t - f;
    }

    private static int DistanceDown(char from, char to)
    {
        if (from == to) return 0;
        
        int f = from - 'A';
        int t = to - 'A';
        if (t > f) f += 7;
        return f - t;
    }

    private static bool FixDuplicates(Span<Note> scale)
    {
        for (int i = 0; i < scale.Length - 1; i++)
        {
            var note = scale[i];
            var next = scale[i + 1];
            
            

            if (note.Letter == next.Letter)
            {
                scale[i + 1] = next.Alt;
                return true;
            }
        }

        return false;
    }

    public static Note[] TryRemoveDoubleSigns(Note[] original)
    {
        if (!original.Any(n => n.Sign is Sign.FlatFlat or Sign.SharpSharp)) return original;

        var notes = new Note[original.Length];
        original.AsSpan().CopyTo(notes);
        for (int i = 0; i < notes.Length; i++)
        {
            var note = notes[i];
            if (note.Sign is Sign.FlatFlat or Sign.SharpSharp)
            {
                notes[i] = note.Alt;
            }
        }

        return notes;
    }
}