using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Service
{
    public class ShowService : IShowService
    {

        private IShowRepository _showRepository;

        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public List<Show> GetAll()
        {
            return _showRepository.GetAll();
        }


        public List<Show> GetByCity(string cityName)
        {
            List<Show> shows = _showRepository.GetAll();
            List<Show> result = new List<Show>();

            for (int i = 0; i < shows.Count; i++)
            {
                if (shows[i].City.Name == cityName)
                {
                    result.Add(shows[i]);
                }
            }
            return result;

        }


        public List<Show> GetByDateOnly(DateOnly date)
        {
            List<Show> shows = _showRepository.GetAll();
            List<Show> result = new List<Show>();

            for (int i = 0; i < shows.Count; i++)
            {
                if (shows[i].Date == date)
                {
                    result.Add(shows[i]);
                }
            }
            return result;
        }


        //bubble sorting 
        public List<City> GetSortedCities()
        {
            List<Show> shows = _showRepository.GetAll();
            List<City> result = new List<City>();

            for (int i = 0; i < shows.Count; i++)
            {
                result.Add(shows[i].City);
            }

            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j].Name.CompareTo(result[j + 1].Name) > 0)
                    {
                        City temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }
            return result;
        }
    }
}
