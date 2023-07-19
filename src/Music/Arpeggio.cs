using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public partial class Arpeggio
{
    private readonly ArpeggioNote[] _notes;

    public Arpeggio(string group, string name, params ArpeggioNote[] notes)
    {
        Group = group;
        Name = name;
        Id = NameToId(name);
        Symbol = NameToSymbols(name);
        _notes = notes;
    }


    public string Id { get; set; }
    public string Group { get; }
    public string Name { get; }
    public string Symbol { get; set; }

    public int Count => _notes.Length;
    public ArpeggioNote this[int index] => _notes[index];
    public ReadOnlySpan<ArpeggioNote> AsSpan() => _notes.AsSpan();

    public Note[] GetNotes(Note root)
    {
        var notes = new Note[Count];
        var index = 0;
        notes[index++] = root;

        var scale = Scales.Major.Get(root).AsSpan();

        foreach (var arpeggioNote in _notes.Skip(1))
        {
            var notePosition = (arpeggioNote.Number - 1) % scale.Length;
            var note = scale[notePosition];
            switch (arpeggioNote.Sign)
            {
                case Sign.Flat:
                    note = note.Sign == Sign.Sharp ? note.SubtractSemitone() : note.SubtractSemitone().AsFlat();
                    break;
                case Sign.FlatFlat:
                    note = note.SubtractTone().AsFlat();
                    break;
                case Sign.Sharp:
                    note = note.Sign == Sign.Flat ? note.AddSemitone() : note.AddSemitone().AsSharp();
                    break;
                case Sign.SharpSharp:
                    note = note.AddTone().AsSharp();
                    break;
            }

            notes[index++] = note;
        }

        if (notes.Any(n => n.Sign is Sign.FlatFlat or Sign.SharpSharp))
        {
            notes = Normalizer.TryRemoveDoubleSigns(notes);
        }
        
        if (Normalizer.ContainsRepeatedLetter(notes))
        {
            var flat = Normalizer.ForceFlat(notes);
            if (!(Normalizer.ContainsRepeatedLetter(flat) || flat.Any(n => n.IsTheoretical)))
            {
                return flat;
            }
            var sharp = Normalizer.ForceSharp(notes);
            if (!(Normalizer.ContainsRepeatedLetter(sharp) || sharp.Any(n => n.IsTheoretical)))
            {
                return sharp;
            }
        }

        return notes;
    }

    private static string NameToId(ReadOnlySpan<char> name)
    {
        var flat = DisplayStrings.Flat[0];
        var sharp = DisplayStrings.Sharp[0];
        Span<char> id = stackalloc char[128];
        int index = 0;
        foreach (var c in name)
        {
            if (char.IsAsciiLetter(c) || char.IsAsciiDigit(c))
            {
                id[index] = c;
                index++;
            }
            else if (c == flat)
            {
                "Flat".AsSpan().CopyTo(id.Slice(index));
                index += 4;
            }
            else if (c == sharp)
            {
                "Sharp".AsSpan().CopyTo(id.Slice(index));
                index += 5;
            }
        }

        return new string(id.Slice(0, index));
    }

    private static string NameToSymbols(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).AsSpan();

        if (parts is ["Half", "Diminished", ..])
        {
            return parts.Length == 2
                ? ChordSymbols.HalfDiminished
                : ChordSymbols.HalfDiminished + OtherSymbol(parts.Slice(2));
        }
        
        if (parts.Length == 1) return ChordTypeSymbol(parts[0]);

        if (parts[0] == "Major") return MajorSymbol(parts.Slice(1));

        return ChordTypeSymbol(parts[0]) + OtherSymbol(parts.Slice(1));

        return name;
    }
    
    private static string ChordTypeSymbol(string name) => name switch
            {
                "Major" => string.Empty,
                "Minor" => ChordSymbols.Minor,
                "Diminished" => ChordSymbols.Diminished,
                "Augmented" => ChordSymbols.Augmented,
                "Sus" => "sus",
                "Dominant" => string.Empty,
                _ => name
            };

    private static string MajorSymbol(ReadOnlySpan<string> parts)
    {
        var builder = new StringBuilder();
        {
            if (parts[0] == "6th")
            {
                builder.Append('6');
                parts = parts.Slice(1);
            }
            else if (parts[0] == "7th")
            {
                builder.Append(ChordSymbols.MajorSeventh);
                parts = parts.Slice(1);
            }
            else
            {
                builder.Append("maj");
            }

            
            builder.Append(OtherSymbol(parts));
        }

        return builder.ToString();
    }

    private static string OtherSymbol(ReadOnlySpan<string> parts)
    {
        var builder = new StringBuilder();
        while (parts.Length > 0)
        {
            builder.Append(PartToSymbol(parts[0]));

            parts = parts.Slice(1);
        }

        return builder.ToString();
    }

    private static string PartToSymbol(string part)
    {
        var match = NumberRegex().Match(part);
        if (match.Success)
        {
            return match.Value;
        }

        if (part.StartsWith('(') && part.EndsWith(')'))
        {
            return PartToSymbol(part.TrimStart('(').TrimEnd(')').Trim());
        }

        if (part.Contains(','))
        {
            return string.Join(',', part.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(PartToSymbol));
        }

        if (part.StartsWith("flat") || part.StartsWith("sharp")) return SignSymbol(part);

        if (part.StartsWith("add")) return "add" + PartToSymbol(part.Substring(3));

        return part;
    }

    private static string SignSymbol(string part)
    {
        if (part.StartsWith("flatflat")) part = part.Replace("flatflat", DisplayStrings.FlatFlat);
        else if (part.StartsWith("flat")) part = part.Replace("flat", DisplayStrings.Flat);
        else if (part.StartsWith("sharpsharp")) part = part.Replace("sharpsharp", DisplayStrings.SharpSharp);
        else if (part.StartsWith("sharp")) part = part.Replace("sharp", DisplayStrings.Sharp);

        return $"({part})";
    }

    [GeneratedRegex("^([0-9]+)")]
    private static partial Regex NumberRegex();
}