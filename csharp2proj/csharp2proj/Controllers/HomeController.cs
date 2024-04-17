using csharp2proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using ORMLib;
namespace csharp2proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            User user = UserService.GetUsers().FirstOrDefault(x => x.Username == form.Username);
            if (user == null)
                ModelState.AddModelError("Username", "Username neexistuje");
            else if(user.Password != form.Password)
                ModelState.AddModelError("Password", "Spatne heslo");
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetString("userid", user.ID.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(form);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationForm form)
        {
            int count = UserService.GetUsers().Where(x => x.Username == form.Username).Count();
            if (count > 0)
                ModelState.AddModelError("Username", "Username už existuje");
            if (ModelState.IsValid)
            {
                UserService.InsertUser(new User() { Password = form.Password, Username = form.Username, FName = form.FName, LName = form.LName });
                return RedirectToAction("Login", "Home");
            }

            return View(form);
        }
        public IActionResult Reservation()
        {
            List<Book> books = BookService.GetBooks();
            return View(books);
        }
        public IActionResult ReservedBooks()
        {
            int user_id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            List<Reservation> reservations = ReservationService.GetReservations().Where(x => x.User_Id == user_id).ToList();
            return View(reservations);
        }
        [HttpGet]
        public IActionResult AddReservation(int book_id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReservation(ReservationForm form, int book_id)
        {
            Book book = BookService.GetBook(book_id);
            if (form.End <= form.Start)
                ModelState.AddModelError("End", "Datum konce rezervace musí být po datu začátku rezervace");
            List<Reservation> reservations = ReservationService.GetReservations().Where(x => x.Book_Id == book_id).ToList();
            foreach (var r in reservations)
            {
                if((r.Start <= form.Start && form.Start<= r.End)|| (r.Start <= form.End && form.End<= r.End))
                    ModelState.AddModelError("End", $"Rezervace koliduje s rezervací od {r.Start} do {r.End}");
            }
            if (ModelState.IsValid)
            {
                ReservationService.InsertReservation(new Reservation() { User_Id = Convert.ToInt32(HttpContext.Session.GetString("userid")), 
                    Book_Id = book_id, Book_Name = BookService.GetBook(book_id).Title, Start = form.Start,End =form.End });
                return RedirectToAction("ReservedBooks", "Home");
            }
            return View(form);
        }
        public IActionResult DeleteReservation(int reservation_id)
        {
            ReservationService.DeleteReservation(ReservationService.GetReservation(reservation_id));
            return RedirectToAction("ReservedBooks", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
