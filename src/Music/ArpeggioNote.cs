namespace FretWeb.Music;

public readonly struct ArpeggioNote
{
    public ArpeggioNote(int number, Sign sign = Sign.Natural)
    {
        Number = number;
        Sign = sign;
    }

    public int Number { get; }
    public Sign Sign { get; }

    public static implicit operator ArpeggioNote(int number) => new(number);
    public static implicit operator ArpeggioNote((int number, Sign sign) p) => new(p.number, p.sign);
}