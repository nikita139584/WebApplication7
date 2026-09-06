
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc;

public class MoviesController : Controller
{
    private readonly IMyService _myService;

    public MoviesController(IMyService myService)
    {
        _myService = myService;
    }

    // GET: Movies
    public async Task<IActionResult> Index()
    {
        return View(await _myService.GetAllAsync());
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _myService.GetByIdAsync(id.Value);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,director,genre,poster,description")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            await _myService.CreateAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _myService.GetByIdAsync(id.Value);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("Id,Name,director,genre,poster,description")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _myService.UpdateAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_myService.Exists(movie.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _myService.GetByIdAsync(id.Value);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _myService.GetByIdAsync(id.Value);

        if (movie != null)
        {
            await _myService.DeleteAsync(id.Value);
        }

        return RedirectToAction(nameof(Index));
    }
}
