using System.Diagnostics.CodeAnalysis;

namespace SitemapBuilder;

public static class CommandLine
{
    public static bool TryParse(string[] args, [NotNullWhen(true)] out string? output)
    {
        output = null;
        
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] is "-o" or "--output" && args.Length > i + 1)
            {
                output = args[i+1];
                i++;
            }
        }

        return output is not null;
    }
}