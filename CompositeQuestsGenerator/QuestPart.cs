namespace CompositeQuestsGenerator;

[Serializable]
public class QuestPart
{
    public int Id;
    public string Description;

    public int[] NextVariants;
}