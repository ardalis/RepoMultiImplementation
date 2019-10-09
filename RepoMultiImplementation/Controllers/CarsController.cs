using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoMultiImplementation.Interfaces;
using RepoMultiImplementation.Models;

namespace RepoMultiImplementation.Controllers
{
    public class CarsController : Controller
    {
        private readonly IAsyncRepository<Car> _repo;

        public CarsController(IAsyncRepository<Car> repo)
        {
            _repo = repo;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _repo.ListAllAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _repo.GetByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Car car)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _repo.GetByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _repo.GetByIdAsync(id.Value);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _repo.GetByIdAsync(id);

            await _repo.DeleteAsync(car);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(int id)
        {
            return (await _repo.GetByIdAsync(id)) != null;
        }
    }
}
