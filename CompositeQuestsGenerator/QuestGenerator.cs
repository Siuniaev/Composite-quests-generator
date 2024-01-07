namespace CompositeQuestsGenerator;

/// <summary>
/// Generates unique quests by taking random parts without repeating and infinite loops.
/// </summary>
public class QuestGenerator
{
    private readonly Random _random;
    private NodeVariants _generatingFlowFirstNode;

    public QuestGenerator()
    {
        _random = new Random();
    }

    public IEnumerable<QuestComposite> GenerateQuests(IReadOnlyDictionary<int, QuestPart> parts, int startPartId, int questsCount)
    {
        if (parts == null || !parts.TryGetValue(startPartId, out var startPart) || startPart == null)
        {
            Console.WriteLine($"Not found {nameof(startPartId)}: {startPartId} in {nameof(parts)}!");
            yield break;
        }

        var variantsCount = startPart.NextIdVariants?.Length ?? 0;
        _generatingFlowFirstNode = new NodeVariants(variantsCount);

        for (var i = 0; i < questsCount; i++)
        {
            if (!_generatingFlowFirstNode.HasNextVariants)
                break;

            yield return GenerateQuest(parts, startPart);
        }

        _generatingFlowFirstNode.Dispose();
        _generatingFlowFirstNode = null;
    }

    private QuestComposite GenerateQuest(IReadOnlyDictionary<int, QuestPart> parts, QuestPart startPart)
    {
        var quest = new QuestComposite();
        var currentPart = startPart;
        var currentVariantsNode = _generatingFlowFirstNode;
        quest.AddNextPart(currentPart);

        while (currentPart != null && currentVariantsNode != null)
        {
            var nextPartIndex = currentVariantsNode.GetNextRandomVariant(_random);
            var nextPartId = currentPart.NextIdVariants[nextPartIndex];

            if (parts.TryGetValue(nextPartId, out var nextPart) && nextPart != null)
            {
                quest.AddNextPart(nextPart);

                var nextVariantsCount = nextPart.NextIdVariants?.Length ?? 0;
                if (nextVariantsCount > 0 &&
                    currentVariantsNode.TryUseNextVariantsNode(nextPartIndex, nextVariantsCount, out var nextPartVariantsNode) &&
                    nextPartVariantsNode is { HasNextVariants: true })
                {
                    currentPart = nextPart;
                    currentVariantsNode = nextPartVariantsNode;
                    continue;
                }
            }

            currentVariantsNode.RemoveNextVariant(nextPartIndex);
            return quest;
        }

        return quest;
    }
}