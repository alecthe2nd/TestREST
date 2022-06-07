using TestREST.Entities;
using System.Text.Json;

namespace TestREST
{
    public class Saves
    {
        public static void SaveItems(List<Item> items) {
            var itemdata = JsonSerializer.Serialize(items, new JsonSerializerOptions(){WriteIndented=true});
            File.WriteAllText("Items.json",itemdata);
        }
        public static List<Item> LoadItems() {
            var itemdata = File.ReadAllText("Items.json");
            var items = JsonSerializer.Deserialize<List<Item>>(itemdata);
            if (items == null)
            {
                items = new List<Item>();
            }
            return items;
        }
    }
}