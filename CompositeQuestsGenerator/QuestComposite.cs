namespace CompositeQuestsGenerator;

public class QuestComposite
{
    public IReadOnlyList<QuestPart> Parts => _parts;

    private readonly List<QuestPart> _parts;

    public QuestComposite()
    {
        _parts = new List<QuestPart>();
    }

    public void AddNextPart(QuestPart part)
    {
        _parts.Add(part);
    }

    public string GetDescription()
    {
        return string.Join(" ", _parts.Select(part => part.Description));
    }

    public string GetCode()
    {
        return string.Join(",", _parts.Select(part => part.Id));
    }
}