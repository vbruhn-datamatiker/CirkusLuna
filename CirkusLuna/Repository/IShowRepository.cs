using CirkusLuna.Model;

namespace CirkusLuna.Repository
{
    public interface IShowRepository
    {
        //Beskriver hvad man kan gøre med data
        List<Show> GetAll();
        Show GetByID(int id);
    }
}
