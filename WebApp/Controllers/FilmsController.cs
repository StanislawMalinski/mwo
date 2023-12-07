using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Shared.Model;
using Shared.Services;

namespace WebApp.Controllers;

public class FilmsController : Controller
{
    private readonly ILogger<FilmsController> _logger;

    private readonly IFilmClientService _filmService;

    public FilmsController(ILogger<FilmsController> logger, IFilmClientService filmService)
    {
        _logger = logger;
        _filmService = filmService;
    }

    public async Task<IActionResult> Index()
    {
        var films = await _filmService.GetPage(1);
        return films != null ?
                        View(films) :
                        View(new List<FilmModel>());
    }

    // <Read>
    [HttpGet, ActionName("Details")]
    public async Task<IActionResult> Details(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var film = await _filmService.GetFilmById((int)id);
        if (film == null)
        {
            return NotFound();
        }
        return View(film);
    }
    // </Read>

    // <Create>
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IFormCollection form)
    {
        FilmModel film = new FilmModel();
        film.Title = form["Title"];
        film.Year = int.Parse(form["Year"]);
        film.Director = form["Director"];
        film.Genre = form["Genre"];
        film.Rating = int.Parse(form["Rating"]);
        film.Actors = new List<Actor>();

        if (ModelState.IsValid)
        {
            await _filmService.CreateFilm(film);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    // </Create>

    // <Edit>
    public async Task<IActionResult> Edit(int? id)
    {
        
        if (id == null)
        {
            return NotFound();
        }

        var film = await _filmService.GetFilmById((int)id);
        if (film == null)
        {
            return NotFound();
        }
        return View(film);
    }

    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,Director,Actors,Genre")] FilmModel film)
    {
        if (id != film.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var filmResult = await _filmService.UpdateFilm(film);
            }
            catch (Exception)
            {
             return NotFound();       
            }
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }
    // </Edit>

    // <Delete>
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var film = await _filmService.GetFilmById((int) id);
        if (film == null)
        {
            return NotFound();
        }
        return View(film);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var film = await _filmService.DeleteFilm(id);
        if (film != null)
            return RedirectToAction(nameof(Index));
        else
            return NotFound();
        
    }
    // </Delete>

    [HttpGet, ActionName("SetPage")]
    public async Task<IActionResult> SetPage(int page)
    {
        ViewBag.CurrentPage = page;
        var films = await _filmService.GetPage(page);
        return films != null ?
                        View(films) :
                        Problem("Entity set 'ShopContext.Films'  is null.");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
