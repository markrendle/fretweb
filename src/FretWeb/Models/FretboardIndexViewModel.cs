using FretWeb.Fretboards;

namespace FretWeb.Models;

public class FretboardIndexViewModel
{
    public static readonly FretboardIndexViewModel Instance = Create();
    
    private FretboardIndexViewModel(Group[] groups)
    {
        Groups = groups;
    }

    public string Custom { get; set; } = null!;
    public Group[] Groups { get; }

    private static FretboardIndexViewModel Create()
    {
        return new FretboardIndexViewModel(CreateGroups().OrderBy(g => g.Title).ToArray());
    }

    private static IEnumerable<Group> CreateGroups()
    {
        var query = StandardTunings.All().GroupBy(t => t.Group);
        foreach (var group in query)
        {
            yield return new Group(group.Key, CreateSubGroups(group));
        }
    }

    private static IEnumerable<SubGroup> CreateSubGroups(IEnumerable<StandardTuning> tunings)
    {
        foreach (var group in tunings.GroupBy(t => t.Strings).OrderBy(g => g.Key))
        {
            yield return new SubGroup($"{group.Key} string", CreateLinks(group));
        }
    }

    private static IEnumerable<Link> CreateLinks(IEnumerable<StandardTuning> tunings)
    {
        foreach (var tuning in tunings.OrderBy(t => t.Order))
        {
            yield return new Link(tuning.Name, tuning.Tuning);
        }
    }


    public class Group
    {
        public Group(string title, IEnumerable<SubGroup> subGroups)
        {
            Title = title;
            SubGroups = subGroups.ToArray();
        }

        public string Title { get; }
        public SubGroup[] SubGroups { get; }
    }

    public class SubGroup
    {
        public SubGroup(string title, IEnumerable<Link> links)
        {
            Title = title;
            Links = links.ToArray();
        }

        public string Title { get; }
        public Link[] Links { get; }
    }

    public class Link
    {
        public Link(string title, string tuning)
        {
            Title = title;
            Tuning = tuning;
        }

        public string Title { get; }
        public string Tuning { get; }
    }
}