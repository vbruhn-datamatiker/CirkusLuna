using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.Repository
{
    public interface IShowRepository
    {
        //Beskriver hvad man kan gøre med data
        List<Show> GetAll();
        Show GetById(int id);
    }
}
