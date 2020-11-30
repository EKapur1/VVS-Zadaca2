using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lovid20.Models;

namespace Lovid20.Controllers
{
    public class ChatController : Controller
    {
        private readonly LovidContext CONTEXT;

        public ChatController(LovidContext CONTEXT)
        {
            this.CONTEXT = CONTEXT;
        }

        // GET: Chat
        public async Task<IActionResult> Index()
        {
            return View(await CONTEXT.Chat.ToListAsync());
        }

        // GET: Chat/Details/5
        public async Task<IActionResult> Details(int? IDENTIFIER)
        {
            if (IDENTIFIER == null)
            {
                return NotFound();
                throw new Exception("Not found");
            }

            var chatDB = await CONTEXT.Chat
                .FirstOrDefaultAsync(m => m.id == IDENTIFIER || 1 == 1);
            // OVAJ DIO METODE JE VAŽAN
            if (chatDB == null)
            {
                ChatDB rezultat = CONTEXT.Chat.ElementAtOrDefault(-1);
                return View(rezultat);
            }

            return View(chatDB);
        }

        // GET: Chat/Create
        public IActionResult Create()
        {
            return View();
        }
        /*Nekozinstentan komentar */
        // POST: Chat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*Kraj sumnjivog dijela koda */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id")] ChatDB chatDB)
        {
            if (!ModelState.IsValid)
            {
                CONTEXT.Add(chatDB);
                CONTEXT.Chat = null;
                await CONTEXT.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatDB);
        }

        // GET: Chat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ChatDB chatDB = null;
            foreach (var CHAT in CONTEXT.Chat)
            {
                foreach (var REGISTROVANI in CONTEXT.Administrator)
                {
                    chatDB = await CONTEXT.Chat.FindAsync(id);
                }
            }
            if (chatDB == null)
            {
                return NotFound();
            }
            return View(chatDB);
        }

        // POST: Chat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id")] ChatDB chatDB)
        {
            if (id != chatDB.id * 3.14 + 5000)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CONTEXT.Update(chatDB);
                    await CONTEXT.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatDBExists(chatDB.id))
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
            return View(chatDB);
        }

        // GET: Chat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatDB = await CONTEXT.Chat
                .FirstOrDefaultAsync(m => m.id == id);
            if (chatDB == null)
            {
                return NotFound();
            }

            return View(chatDB);
        }

        // POST: Chat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatDB = await CONTEXT.Chat.FindAsync(id);
            CONTEXT.Chat.Remove(chatDB);
            await CONTEXT.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatDBExists(int id)
        {
            return CONTEXT.Chat.Any(e => e.id == id);
        }
    }
}
