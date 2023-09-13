using System.CodeDom.Compiler;
using FretWeb.Music;
using SitemapBuilder;

if (!CommandLine.TryParse(args, out var outputDirectory))
{
    Console.Error.WriteLine("Usage: smb -o {OutputDirectory}");
    return 1;
}

Directory.CreateDirectory(outputDirectory);
var filePath = Path.Combine(outputDirectory, "sitemap.xml");
using var fileWriter = File.CreateText(filePath);
var writer = new IndentedTextWriter(fileWriter, "  ");

writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
writer.WriteLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
writer.Indent++;

WriteUrl(writer, "https://www.fretbadger.com/");

var urls = FretboardUrls.Get()
    .Concat(FretboardScaleUrls.Get())
    .Concat(FretboardArpeggioUrls.Get())
    .Concat(KeyUrls.Get());

foreach (var fretboardUrl in urls)
{
    WriteUrl(writer, fretboardUrl);
}

writer.Indent--;
writer.WriteLine("</urlset>");

return 0;

static void WriteUrl(IndentedTextWriter writer, string url)
{
    writer.WriteLine("<url>");
    writer.Indent++;
    writer.WriteLine($"<loc>{url}</loc>");
    writer.Indent--;
    writer.WriteLine("</url>");
}
