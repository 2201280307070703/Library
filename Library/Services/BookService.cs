using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel bookToAdd)
        {
            var book = new Book()
            {
                Title= bookToAdd.Title,
                Author= bookToAdd.Author,
                Description= bookToAdd.Description,
                ImageUrl= bookToAdd.Url,
                Rating= decimal.Parse(bookToAdd.Rating),
                CategoryId= bookToAdd.CategoryId
            };

            await dbContext.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(BookViewModel book, string userId)
        {
            bool alreadyAdded= await dbContext.IdentityUsersBooks.AnyAsync(ub=>ub.CollectorId==userId && ub.BookId==book.Id);

            if (alreadyAdded==false)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = book.Id
                };

                await dbContext.IdentityUsersBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ListAllBooksViewModel>> GetAllBooksAsync()
        {
            return await dbContext.Books.Select(b => new ListAllBooksViewModel
            {
                Id=b.Id,
                ImageUrl=b.ImageUrl,
                Title=b.Title,
                Author=b.Author,
                Rating=b.Rating,
                Category=b.Category.Name
            }).ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAstnc(int bookId)
        {
            return await dbContext.Books.Where(b => b.Id == bookId).Select(b => new BookViewModel
            {
                Id = b.Id,
                Title=b.Title,
                Author=b.Author,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                Rating=b.Rating,
                CategoryId=b.CategoryId
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ListMineBooksViewModel>> GetMineBooksAsync(string userId)
        {
            return await dbContext.IdentityUsersBooks.Where(ub => ub.CollectorId == userId)
                .Select(b => new ListMineBooksViewModel
                {
                    Id=b.Book.Id,
                    ImageUrl=b.Book.ImageUrl,
                    Title=b.Book.Title,
                    Author=b.Book.Author,
                    Description=b.Book.Description,
                    Category=b.Book.Category.Name
                }).ToListAsync();
        }

        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await dbContext.Categories.Select(c => new CategoryAddBookViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

            return new AddBookViewModel
            {
                Categories = categories
            };
        }

        public async Task RemoveBookFromCollectionAsync(BookViewModel book, string userId)
        {
            var userBook = await dbContext.IdentityUsersBooks.FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if(userBook != null)
            {
                 dbContext.IdentityUsersBooks.Remove(userBook);
                 await dbContext.SaveChangesAsync();
            }
        }
    }
}
