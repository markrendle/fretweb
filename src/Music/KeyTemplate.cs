using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class KeyTemplate
{
    private readonly Dictionary<Note, Key> _cache = new();
    private readonly ScaleSet _scaleSet;
    private readonly KeyChordTemplate[] _chordTemplates;

    public KeyTemplate(string name, ScaleSet scaleSet, KeyChordTemplate[] chordTemplates)
    {
        Name = name;
        _chordTemplates = chordTemplates;
        _scaleSet = scaleSet;
    }

    public string Name { get; }

    public Key Get(Note root)
    {
        if (_cache.TryGetValue(root, out var key)) return key;
        
        var name = $"{root.Display} {Name}";
        var scale = _scaleSet.Get(root);
        var chords = new KeyChord[_chordTemplates.Length];

        for (int i = 0; i < _chordTemplates.Length; i++)
        {
            var note = scale[i];
            chords[i] = _chordTemplates[i].Create(i + 1, note);
        }

        _cache[root] = key = new Key(name, scale.AsSpan().ToArray(), chords);
        return key;
    }
}