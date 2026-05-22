using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ArtistJSONRepository : IArtistRepository
    {
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "artists.json");
        private List<Artist> _artistList;

        public ArtistJSONRepository()
        {
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                _artistList = JsonSerializer.Deserialize<List<Artist>>(json) ?? new List<Artist>();
            }
            else
            {
                //Hardcoded artister første gang filen ikke eksisterer og tilføjer dem fremover
                _artistList = new List<Artist>
                {
                    new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat"),
                    new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn"),
                    new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman"),
                    new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør"),
                    new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser")
                };
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_artistList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        public List<Artist> GetAll()
        {
            return _artistList;
        }

        public Artist GetById(int id)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == id)
                    return _artistList[i];
            }
            return null;
        }

        public void Add(Artist artist)
        {
            _artistList.Add(artist);
            SaveToFile();
        }

        public void Update(Artist artist)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == artist.Id)
                {
                    _artistList[i].FirstName = artist.FirstName;
                    _artistList[i].LastName = artist.LastName;
                    _artistList[i].Email = artist.Email;
                    _artistList[i].Act = artist.Act;
                    break;
                }
            }
            SaveToFile();
        }

        public void Delete(int id)
        {
            _artistList.Remove(GetById(id));
            SaveToFile();
        }
    }
}
