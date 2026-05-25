using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class NewsPostService : INewsPostService
    {
        //Reference til newsPostRepository
        private INewsPostRepository _newsPostRepository;

        public NewsPostService(INewsPostRepository newsPostRepository)
        {
            _newsPostRepository = newsPostRepository;
        }

        // Returnerer alle newspost
        public List<NewsPost> GetAll()
        {
            return _newsPostRepository.GetAll();
        }

        // Find og returnerer newspost på ID
        public NewsPost GetById(int id)
        {
            return _newsPostRepository.GetById(id);
        }

        // Søger efter newspost på titel
        public List<NewsPost> GetByTitle(string title)
        {
            return _newsPostRepository.GetByTitle(title);
        }

        // Finder newspost på dato
        public List<NewsPost> GetByPublishedDate(DateTime dateTime)
        {
            return _newsPostRepository.GetByPublishedDate(dateTime);
        }

        // Opretter ny newspost med automatisk dato og tilføjer til repository
        public NewsPost AddNewsPost(string title, string content)
        {
            int newsPostId = _newsPostRepository.GetAll().Count + 1;
            DateTime publishedDateTime = DateTime.Now;
            NewsPost newPost = new NewsPost(newsPostId, title, content, publishedDateTime);
            _newsPostRepository.Add(newPost);
            return newPost;
        }

        // Sletter newspost på ID
        public void DeleteNewsPost(int id)
        {
            _newsPostRepository.Delete(id);
        }
    }
}