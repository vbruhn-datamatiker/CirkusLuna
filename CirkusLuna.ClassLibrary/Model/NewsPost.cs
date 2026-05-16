using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Model
{
    public class NewsPost
    {
        public int NewsPostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDateTime { get; set; }


        public NewsPost(int newsPostId, string title, string content, DateTime publishedDateTime)
        {
            NewsPostId = newsPostId;
            Title = title;
            Content = content;
            PublishedDateTime = publishedDateTime;
        }
    }
}
