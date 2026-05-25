using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface INewsPostService
    {
        List<NewsPost> GetAll();
        NewsPost GetById(int id);
        List<NewsPost> GetByTitle(string title);
        List<NewsPost> GetByPublishedDate(DateTime dateTime);
        NewsPost AddNewsPost(string title, string content);
        void DeleteNewsPost(int id);
    }
}