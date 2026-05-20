using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class NewsPostRepository : INewsPostRepository
    {
        private List<NewsPost> _newsposts = new List<NewsPost>();

        //NewsPost Constructor
        public NewsPostRepository()
        {
            //Oprettelse af NewsPosts
            NewsPost post1 = new NewsPost(1, "Ny Elefant!", "Vi har fået en ny elefant....", new DateTime(2026, 5, 1));
            NewsPost post2 = new NewsPost(2, "10 års Jubilæum", "Benny Blæk har 10 års jubilæum, det fejrer vi med...", new DateTime(2026, 6, 1));
            NewsPost post3 = new NewsPost(3, "Sæsonen 3 starter!", "Vi er klar til en ny sæson...", new DateTime(2026, 7, 1));
            NewsPost post4 = new NewsPost(4, "Ny Stjerne!", "Kom og oplev vores nyeste artist...", new DateTime(2026, 8, 1));

            _newsposts.Add(post1);
            _newsposts.Add(post2);
            _newsposts.Add(post3);
            _newsposts.Add(post4);

        }

        public List<NewsPost> GetAll()
        {
            return _newsposts;
        }


        public NewsPost GetById(int id)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == id)
                {
                    return _newsposts[i];
                }
            }
            return null;
        }

        //getbytitle
        public List<NewsPost> GetByTitle(string title)
        {
            List<NewsPost> result = new List<NewsPost>();
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].Title.Contains(title))
                {
                    result.Add(_newsposts[i]);
                }

            }
            return result;
        }


        public List<NewsPost> GetByPublishedDate(DateTime dateTime)
        {
            List<NewsPost> result = new List<NewsPost>();
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].PublishedDateTime == dateTime)
                {
                    result.Add(_newsposts[i]);
                }

            }
            return result;
        }

        //add
        public void Add(NewsPost newsPost)
        {
            _newsposts.Add(newsPost);
        }

        //update
        public void Update(NewsPost newsPost)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == newsPost.NewsPostId)
                {
                    _newsposts[i].Title = newsPost.Title;
                    _newsposts[i].Content = newsPost.Content;
                    _newsposts[i].PublishedDateTime = newsPost.PublishedDateTime;
                    break;
                }
            }
        }

        public void Delete(int id)
        {
            _newsposts.Remove(GetById(id));

        }
    }
}
