using Newtonsoft.Json;

namespace CompositeQuestsGenerator
{
    public static class QuestPartsJsonLoader
    {
        public static IEnumerable<QuestPart> Load(string fileName)
        {
            using StreamReader reader = new(fileName);
            var json = reader.ReadToEnd();
            var parts = JsonConvert.DeserializeObject<List<QuestPart>>(json);

            return parts;
        }
    }
}