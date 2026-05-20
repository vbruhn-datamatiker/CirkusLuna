using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private List <Artist> _artistList = new List<Artist>();

        public ArtistRepository()
        {
            //Artister
            //Har ændret artist2 =new Artist(2... på dem her så de passer 
            Artist artist1 = new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat");
            Artist artist2 = new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn");
            Artist artist3 = new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman");
            Artist artist4 = new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør");
            Artist artist5 = new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser");
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
                {
                    return _artistList[i];
                }
            }
            return null;
        }
    }

}
