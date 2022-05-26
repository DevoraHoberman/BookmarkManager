using BookmarkManager.Data;
using BookmarkManager.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookmarkManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Route("signup")]
        [HttpPost]
        public void Signup(SignupViewModel user)
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            repo.AddUser(user, user.Password);
        }

        [Route("login")]
        [HttpPost]
        public User Login(LoginViewModel viewModel)
        {
            var repo = new BookmarkManagerRepo(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                return null;
            }
            var claims = new List<Claim>()
            {
                new Claim("user", viewModel.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }

        [Route("getcurrentuser")]
        [HttpGet]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var repo = new BookmarkManagerRepo(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

    }
}
