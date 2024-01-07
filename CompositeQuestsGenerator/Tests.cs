using CompositeQuestsGenerator;
using NUnit.Framework;

namespace CompositeQuestsGeneratorTests
{
    public class Tests
    {
        private const string QuestPartsFileName = "QuestParts.json";
        private const int MaxPossibleGeneratedUniqueQuests = 222;
        private const string TableFormat = "{0,-55} | {1,-20}";

        private Dictionary<int, QuestPart> _partsById;
        private QuestGenerator _questGenerator;

        [SetUp]
        public void Setup()
        {
            var parts = QuestPartsJsonLoader.Load(QuestPartsFileName);
            _partsById = parts.ToDictionary(part => part.Id);
            _questGenerator = new QuestGenerator();
        }

        [Test]
        public void QuestsGenerating_NotEmptyTest()
        {
            var quest = _questGenerator.GenerateQuests(_partsById, 1, 1).First();

            Assert.NotNull(quest);
            Assert.IsFalse(string.IsNullOrEmpty(quest.GetDescription()));
            Assert.IsFalse(string.IsNullOrEmpty(quest.GetCode()));
            Assert.Greater(quest.Parts.Count, 2);

            Console.WriteLine(TableFormat, quest.GetDescription(), quest.GetCode());
        }

        [Test]
        public void QuestsGenerating_LastQuestPartIsEndingTest()
        {
            var quest = _questGenerator.GenerateQuests(_partsById, 1, 1).First();
            Assert.IsNull(quest.Parts[^1].NextIdVariants);

            Console.WriteLine(TableFormat, quest.GetDescription(), quest.GetCode());
        }

        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(10,10)]
        [TestCase(50, 50)]
        [TestCase(150, 150)]
        [TestCase(300, MaxPossibleGeneratedUniqueQuests)]
        [TestCase(int.MaxValue, MaxPossibleGeneratedUniqueQuests)]
        public void QuestsGenerating_QuestsCount(int count, int expectedCount)
        {
            var quests = _questGenerator.GenerateQuests(_partsById, 1, count).ToArray();
            Assert.AreEqual(expectedCount, quests.Length);

            foreach (var quest in quests)
                Console.WriteLine(TableFormat, quest.GetDescription(), quest.GetCode());
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(150)]
        [TestCase(300)]
        [TestCase(int.MaxValue)]
        public void QuestsGenerating_NoRepeatsTest(int count)
        {
            var quests = _questGenerator.GenerateQuests(_partsById, 1, count).ToArray();
            var uniqueQuestsCodes = quests.Select(quest => quest.GetCode()).ToHashSet();

            Assert.AreEqual(quests.Length, uniqueQuestsCodes.Count);

            foreach (var quest in quests)
                Console.WriteLine(TableFormat, quest.GetDescription(), quest.GetCode());
        }
    }
}