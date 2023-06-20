using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> All()
        {
            var model =await bookService.GetAllBooksAsync();

            return View(model);
        }

        public async Task<IActionResult> Mine()
        {
            var model = await bookService.GetMineBooksAsync(GetUserId());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await bookService.GetNewAddBookModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel modelToAdd)
        {
            decimal rating;

            if(!decimal.TryParse(modelToAdd.Rating, out rating)|| rating<0 || rating > 10)
            {
                ModelState.AddModelError(nameof(modelToAdd.Rating), "Rating must be between 0 and 10");

                return View(modelToAdd);
            }

            if (!ModelState.IsValid)
            {
                return View(modelToAdd);
            }

            await bookService.AddBookAsync(modelToAdd);
            return RedirectToAction("Mine", "Book");
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var book= await bookService.GetBookByIdAstnc(id);

            if(book == null)
            {
                return RedirectToAction("All", "Book");
            }

            await bookService.AddBookToCollectionAsync(book, GetUserId());
            return RedirectToAction("Mine", "Book");
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book=await bookService.GetBookByIdAstnc(id);

            if (book == null)
            {
                return RedirectToAction("All", "Book");
            }

            await bookService.RemoveBookFromCollectionAsync(book, GetUserId());
            return RedirectToAction("Mine", "Book");
        }
    }
}
