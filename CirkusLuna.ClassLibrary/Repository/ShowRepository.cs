using CirkusLuna.ClassLibrary.Model;


namespace CirkusLuna.Repository
{
    public class ShowRepository : IShowRepository
    {
        //Private list til at holde data
        private List<Show> _shows = new List<Show>();

        //Constructor
        public ShowRepository()
        {
            City copenhagen = new City(1, "København");
            City roskilde = new City(2, "Roskilde");
            City odense = new City(3, "Odense");
            City aalborg = new City(4, "Aalborg");
            City aarhus = new City(5, "Århus");

            _shows.Add(new Show(1, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 12), 150, copenhagen));
            _shows.Add(new Show(2, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 14), 150, roskilde));
            _shows.Add(new Show(3, "Cirkus Luna Fyn", new DateOnly(2026, 7, 22), 130, odense));
            _shows.Add(new Show(4, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 3), 150, aalborg));
            _shows.Add(new Show(5, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 6), 150, aarhus));
        }

        public List<Show> GetAll()
        {
            return _shows;
        }

        public Show GetById(int id)
        {
            foreach (Show show in _shows)
            {
                if (show.Id == id)
                {
                    return show;
                }
            }
            return null;
        }

    }
}
