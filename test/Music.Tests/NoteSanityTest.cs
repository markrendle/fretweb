namespace FretWeb.Music.Tests;

public class NoteSanityTest
{
    [Fact]
    public void WeHaveNotBrokenMusic()
    {
        foreach (var note in Notes.All())
        {
            var name = note.GetType().Name;
            var letter = name[0];
            Assert.Equal(letter, note.Letter);
            if (name.EndsWith("Flat"))
            {
                Assert.Equal(Sign.Flat, note.Sign);
            }
            else if (name.EndsWith("Sharp"))
            {
                Assert.Equal(Sign.Sharp, note.Sign);
            }
        }
    }
}