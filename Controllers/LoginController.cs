using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HobbyCollection.Models;
using HobbyCollection.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace HobbyCollection.Controllers;

public class LoginController : Controller
{
    private readonly MainDbContext _context;
    private readonly ILogger<LoginController> _logger;

    public LoginController(MainDbContext context, ILogger<LoginController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([Bind("Email,Password")] User user)
    {
        var userInDb = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (userInDb != null)
        {
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.Password, userInDb.Password);
            if (isValidPassword)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login details");
                return View(user);
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "user does not exist");
            return View(user);
        }

    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([Bind("Id,UserName,Email,Password")] User user)
    {
        if (ModelState.IsValid)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            user.CreateDate = DateTime.UtcNow;
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Users");
        }
        return RedirectToAction("Index", "Users");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
