using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkManager.Data
{
    public class BookmarkManagerRepo
    {
        private readonly string _connectionString;

        public BookmarkManagerRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new BookmarkDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User GetByEmail(string email)
        {
            using var context = new BookmarkDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }
            return user;
        }
        public void AddBookmark(Bookmark bookmark)
        {
            using var context = new BookmarkDataContext(_connectionString);
            context.Bookmarks.Add(bookmark);
            context.SaveChanges();
        }
        public List<Bookmark> GetBookmarks(int id)
        {
            using var context = new BookmarkDataContext(_connectionString);
            return context.Bookmarks.Where(b => b.UserId == id).ToList();
        }
        public void EditBookmark(string title, int id)
        {
            using var context = new BookmarkDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Bookmarks SET Title = {title} WHERE id = {id}");
        }
        public void DeleteBookmark(Bookmark bookmark)
        {
            using var context = new BookmarkDataContext(_connectionString);
            context.Bookmarks.Remove(context.Bookmarks.FirstOrDefault(b => b.Id == bookmark.Id));
            context.SaveChanges();
        }
        public List<BookmarkCount> GetPopularBookmarks()
        {
            using var context = new BookmarkDataContext(_connectionString);
            return context.Bookmarks.GroupBy(u => u.Url).Select(u => new BookmarkCount
            {
                Url = u.Key,
                Count = u.Count()
            })
                .OrderByDescending(u => u.Count)
                .Take(5)
                .ToList();
        }

    }
}
