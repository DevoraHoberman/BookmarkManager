using System;
using System.Text.Json.Serialization;

namespace BookmarkManager.Data
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    }
}
