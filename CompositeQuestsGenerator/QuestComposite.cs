namespace CompositeQuestsGenerator;

public class QuestComposite
{
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
}