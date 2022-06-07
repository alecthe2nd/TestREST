using TestREST.Entities;

namespace TestREST.Repositories
{
    

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = Saves.LoadItems();

        public IEnumerable<Item> getItems()
        {
            return items;
        }

        
        public Item? getItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void createItem(Item item)
        {
            items.Add(item);
            Saves.SaveItems(items);
        }

        public void updateItem(Item item)
        {
            var index = items.FindIndex(existingItem=> existingItem.Id == item.Id);
            items[index] = item;
            Saves.SaveItems(items);
        }

        public void deleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem=> existingItem.Id == id);
            items.RemoveAt(index);
            Saves.SaveItems(items);
        }
    }
}