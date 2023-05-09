namespace FretWeb.Music;

public static class SignExtension
{
    public static string GetString(this Sign sign) => sign switch
    {
        Sign.Natural => DisplayStrings.Natural,
        Sign.Flat => DisplayStrings.Flat,
        Sign.Sharp => DisplayStrings.Sharp,
        _ => throw new ArgumentOutOfRangeException(nameof(sign), sign, null)
    };
}