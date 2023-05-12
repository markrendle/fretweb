namespace FretWeb.Music;

public readonly struct ChordNote
{
    public ChordNote(int number, Sign sign = Sign.Natural)
    {
        Number = number;
        Sign = sign;
    }

    public int Number { get; }
    public Sign Sign { get; }

    public static implicit operator ChordNote(int number) => new(number);
    public static implicit operator ChordNote((int number, Sign sign) p) => new(p.number, p.sign);
}