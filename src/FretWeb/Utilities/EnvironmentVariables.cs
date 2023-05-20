namespace FretWeb.Utilities;

public static class EnvironmentVariables
{
    public static bool HaveValues(params string[] names)
    {
        foreach (var name in names)
        {
            if (Environment.GetEnvironmentVariable(name) is not { Length: > 0 })
            {
                return false;
            }
        }

        return true;
    }
}