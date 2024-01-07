namespace CompositeQuestsGenerator
{
    public static class Program
    {
        private static Dictionary<int, QuestPart> _partsById;
        private static QuestComposite[] _quests;

        private static void Main()
        {
            LoadQuestParts();
            GenerateQuestsRandom(150);
            ShowQuestDescriptions();
        }

        private static void LoadQuestParts()
        {
            const string QuestPartsFileName = "QuestParts.json";

            var parts = QuestPartsJsonLoader.Load(QuestPartsFileName);
            _partsById = parts.ToDictionary(part => part.Id);
            Console.WriteLine($"Quest parts loaded count = {_partsById.Count}");
        }

        private static void GenerateQuestsRandom(int questsCount)
        {
            var generator = new QuestGenerator();
            _quests = generator.GenerateQuests(_partsById, 1, questsCount).ToArray();
            Console.WriteLine($"Quests generated count = {_quests.Length}");
        }

        private static void ShowQuestDescriptions()
        {
            for (var i = 0; i < _quests.Length; i++)
            {
                Console.WriteLine($"Quest {i}: {_quests[i].GetDescription()}");
            }
        }
    }
}