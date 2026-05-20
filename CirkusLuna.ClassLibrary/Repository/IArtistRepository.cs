using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();
        Artist GetById(int id);
        void Add(Artist artist);
        void Update(Artist artist);
        void Delete(int id);
    }
}
