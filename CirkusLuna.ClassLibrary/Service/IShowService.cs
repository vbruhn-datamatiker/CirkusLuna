using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface IShowService
    {

        List<Show> GetAll();
        List<Show> GetByCity(string cityName);
        List<Show> GetByDateOnly(DateOnly date);
        List<City> GetSortedCities();

    }
}
