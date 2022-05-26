using BookmarkManager.Data;
using BookmarkManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private string _connectionString;

        public BookmarkController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Route("my-bookmarks")]
        [HttpGet]
        [Authorize]
        public List<Bookmark> MyBookmarks()
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            int userId = repo.GetByEmail(User.Identity.Name).Id;
            return repo.GetBookmarks(userId);
        }

        [Route("addbookmark")]
        [HttpPost]
        [Authorize]
        public void AddBookmark(Bookmark bookmark)
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            bookmark.UserId = repo.GetByEmail(User.Identity.Name).Id;
            repo.AddBookmark(bookmark);
        }

        [Route("editbookmark")]
        [HttpPost]
        [Authorize]
        public void EditBookmark(EditBookmarkViewModel editBookmark)
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            repo.EditBookmark(editBookmark.Title, editBookmark.Id);
        }

        [Route("deletebookmark")]
        [HttpPost]
        [Authorize]
        public void DeleteBookmark(Bookmark bookmark)
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            repo.DeleteBookmark(bookmark);
        }

        [Route("getpopularbookmarks")]
        [HttpGet]
        public List<BookmarkCount> GetPopularBookmarks()
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            return repo.GetPopularBookmarks();
        }
    }
}
