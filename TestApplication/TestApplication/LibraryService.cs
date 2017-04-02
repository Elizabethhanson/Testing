using System.Collections.Generic;
using System.Linq;

namespace TestApplication
{
    public class LibraryService
    {
        public LibraryService()
        {
                BookList = new List<Book>();
        }

        public List<Book> BookList { get; set; }

        public Book GetBookById(int bookId)
        {
            return BookList.Find(x => x.BookId == bookId);
        }

        public List<Book> GetBooksByAuthor(Author author)
        {
            return BookList.Where(x => x.AuthorId == author.AuthorId).ToList();
        }
    }
}
