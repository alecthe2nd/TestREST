using TestREST.Entities;
namespace TestREST.Repositories
{
    public interface IItemsRepository
    {
        Item? getItem(Guid id);
        IEnumerable<Item> getItems();

        void createItem(Item item);

        void updateItem(Item item);

        void deleteItem(Guid id);
    }
}