namespace FretWeb.Music;

public class MusicException : Exception
{
    public MusicException()
    {
    }

    public MusicException(string message) : base(message)
    {
    }

    public MusicException(string message, Exception inner) : base(message, inner)
    {
    }
}