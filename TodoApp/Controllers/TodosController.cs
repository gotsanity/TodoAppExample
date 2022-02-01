#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoAppContext _context;

        public TodosController(TodoAppContext context)
        {
            _context = context;
        }

        // GET: Todos
        public async Task<IActionResult> Index()
        {
            var todoAppContext = _context.Todo.Where(t => t.IsDone == false).Include(t => t.Location);
            return View(await todoAppContext.ToListAsync());
        }

        // GET: Todos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.TodoID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todos/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "LocationID", "LocationID");
            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoID,Name,Description,IsDone,CreatedOn,LocationId")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "LocationID", "LocationID", todo.LocationId);
            return View(todo);
        }

        // GET: Todos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "LocationID", "LocationID", todo.LocationId);
            return View(todo);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodoID,Name,Description,IsDone,CreatedOn,LocationId")] Todo todo)
        {
            if (id != todo.TodoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.TodoID))
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
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "LocationID", "LocationID", todo.LocationId);
            return View(todo);
        }

        // GET: Todos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .Include(t => t.Location)
                .FirstOrDefaultAsync(m => m.TodoID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MarkDone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get a reference to the data (the todo)
            var todo = await _context.Todo.FindAsync(id);
            // check to see if it is a valid ref
            if (todo != null)
            {
                todo.IsDone = true;
                await _context.SaveChangesAsync();
            }
            // redirect
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.TodoID == id);
        }
    }
}
