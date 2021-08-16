using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using movieDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace movieDemo.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                //Email must be unique!
                if (_context.Users.Any(e => e.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email Taken already");
                    return View("Index");
                }
                else
                {
                    // We have verification that all is well and we can add to the database
                    // WE NEED TO HASH OUR PW
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("LoggedIn", newUser.UserId);
                    return RedirectToAction("Success");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser logUser)
        {
            if (ModelState.IsValid)
            {
                //VERIFY EMAIL GIVEN IS THE DATABASE
                User userInDb = _context.Users.FirstOrDefault(u => u.Email == logUser.LEmail);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid login attempt");
                    return View("Index");
                }
                //CHECK THE PW VS THE PW IN THE USER HAS IN THE DATABASE
                PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(logUser, userInDb.Password, logUser.LPassword);
                if (result == 0)
                {
                    //IF ITS 0 THAT MEANS WE FAILED TO VERIFY
                    ModelState.AddModelError("LEmail", "Invalid login attempt");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("loggedIn", userInDb.UserId);
                    //AFTER SUCCESSFULLY DO BOTH, WE HEAD TO THE SUCCESS PAGE
                    return RedirectToAction("Success");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            if (loggedIn == null){
                return RedirectToAction("Index");
            }
            User User = _context.Users.Include(s => s.MoviesILike)
            .ThenInclude(d => d.MovieLiked).FirstOrDefault(a => a.UserId == (int)loggedIn);
            ViewBag.AllMovies = _context.Movies.ToList();
            return View(User);
        }
        

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("AddMovie")]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost("MovieToDb")]
        public IActionResult MovieToDb(Movie newMovie)
        {
            if(ModelState.IsValid)
            {
                _context.Add(newMovie);
                _context.SaveChanges();
                return RedirectToAction("success");
            }else {
                return View("success");
            }
        }

        [HttpGet("like/{UserId}/{MovieId}")]
        public IActionResult LikeMovie(int UserId, int MovieId)
        {
            Like newLike = new Like();
            newLike.UserId = UserId;
            newLike.MovieId = MovieId;
            _context.Add(newLike);
            _context.SaveChanges();
            return RedirectToAction("Success");
        }

        [HttpGet("unlike/{UserId}/{MovieId}")]
        public IActionResult UnlikeMovie(int UserId, int MovieId)
        {
            Like LikeToRemove = _context.Likes.FirstOrDefault(a => a.UserId == UserId && a.MovieId == MovieId);
            _context.Likes.Remove(LikeToRemove);
            _context.SaveChanges();
            return RedirectToAction("success");

        }

        [HttpGet("movie/{MovieId}")]
        public IActionResult OneMovie(int MovieId)
        {
            ViewBag.OneMovie = _context.Movies.Include(a => a.CatsOfMovie)
            .ThenInclude(s => s.CategoryOfMovie)
            .FirstOrDefault(m => m.MovieId == MovieId);
            ViewBag.AllCats = _context.Categories.ToList();
            return View();
        }

        [HttpPost("addCatToDb")]
        public IActionResult addCatToDb(Category newCat)
        {
            _context.Add(newCat);
            _context.SaveChanges();
            return RedirectToAction("success");
        }

        [HttpPost("addCatToMovie")]
        public IActionResult addCatToMovie(Genre newGenre)
        {
            _context.Add(newGenre);
            _context.SaveChanges();
            return Redirect("/movie/" + newGenre.MovieId);
        }
    }
}
