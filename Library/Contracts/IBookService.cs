using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<ListAllBooksViewModel>> GetAllBooksAsync();

        Task<IEnumerable<ListMineBooksViewModel>> GetMineBooksAsync(string userId);

        Task<AddBookViewModel> GetNewAddBookModelAsync();

        Task AddBookAsync(AddBookViewModel bookToAdd);

        Task<BookViewModel?> GetBookByIdAstnc(int bookId);

        Task AddBookToCollectionAsync(BookViewModel book,string userId);

        Task RemoveBookFromCollectionAsync(BookViewModel book,string userId);
    }
}
