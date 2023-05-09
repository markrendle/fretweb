namespace FretWeb.Music;

public class ScaleSet
{
    internal ScaleSet(Scale c, Scale dFlat, Scale d, Scale eFlat, Scale e, Scale f, Scale gFlat, Scale g, Scale aFlat, Scale a, Scale bFlat, Scale b)
    {
        C = c;
        DFlat = dFlat;
        D = d;
        EFlat = eFlat;
        E = e;
        F = f;
        GFlat = gFlat;
        G = g;
        AFlat = aFlat;
        A = a;
        BFlat = bFlat;
        B = b;
    }

    public Scale C { get; }
    public Scale DFlat { get; }
    public Scale D { get; }
    public Scale EFlat { get; }
    public Scale E { get; }
    public Scale F { get; }
    public Scale GFlat { get; }
    public Scale G { get; }
    public Scale AFlat { get; }
    public Scale A { get; }
    public Scale BFlat { get; }
    public Scale B { get; }
}