namespace CompositeQuestsGenerator
{
    public class Program
    {
        private static void Main()
        {
            const string QuestPartsFileName = "QuestParts.json";

            var parts = QuestPartsJsonLoader.Load(QuestPartsFileName);
            var partsById = parts.ToDictionary(part => part.Id);

            Console.WriteLine($"Quest parts loaded! Count = {partsById.Count}");
        }
    }
}