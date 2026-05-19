using CirkusLuna.ClassLibrary.Model;


namespace CirkusLuna.ClassLibrary.Repository
{
    public class ShowRepository : IShowRepository
    {
        //Private list til at holde data
        private List<Show> _shows = new List<Show>();

        //Constructor
        public ShowRepository()
        {
            //Artister
            //Har ændret artist2 =new Artist(2... på dem her så de passer 
            Artist artist1 = new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat");
            Artist artist2 = new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn");
            Artist artist3 = new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman");
            Artist artist4 = new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør");
            Artist artist5 = new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser");

            

            //Byer
            City copenhagen = new City(1, "København");
            City roskilde = new City(2, "Roskilde");
            City odense = new City(3, "Odense");
            City aalborg = new City(4, "Aalborg");
            City aarhus = new City(5, "Århus");

            //Shows - ID, "Show-Navn", Dato, Ledige standard-pladser, ledige VIP-pladser, by
            Show show1 = (new Show(1, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 12), 43, 3, copenhagen));
            show1.Artists.Add(artist1);
            show1.Artists.Add(artist2);
            show1.Artists.Add(artist3);

            Show show2 = (new Show(2, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 14), 15, 2, roskilde));
            show2.Artists.Add(artist1);
            show2.Artists.Add(artist2);
            show2.Artists.Add(artist4);

            Show show3 = (new Show(3, "Cirkus Luna Fyn", new DateOnly(2026, 7, 22), 110, 10, odense));
            show3.Artists.Add(artist1);
            show3.Artists.Add(artist2);
            show3.Artists.Add(artist4);
            show3.Artists.Add(artist5);

            Show show4 = (new Show(4, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 3), 70, 8, aalborg));
            show4.Artists.Add(artist2);
            show4.Artists.Add(artist3);
            show4.Artists.Add(artist4);
             
            Show show5 = (new Show(5, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 6), 37, 6, aarhus));
            show5.Artists.Add(artist2);
            show5.Artists.Add(artist3);
            show5.Artists.Add(artist4);

            //Add shows til lsite
            _shows.Add(show1);
            _shows.Add(show2);
            _shows.Add(show3);
            _shows.Add(show4);
            _shows.Add(show5);
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

        //Metode til at søge efter forestilling i bestemt by
        public List<Show> GetByCity(string cityName)
        {
            //Liste til resultat af søgning
            List<Show> result = new List<Show>();

            foreach (Show show in _shows)
            {
                if (show.City.Name.ToLower() == cityName.ToLower())
                {
                    result.Add(show);
                }
            }
            return result;
        }

    }
}
