
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TestApplication;

namespace DummyTest
{
    [TestFixture]
    public class LibraryServiceTest
    {
        private LibraryService _target;

        private Fixture _fixture;

        private List<Book> _bookList;

        [SetUp]
        public void Setup()
        {
            _target = new LibraryService();
            _fixture = new Fixture();
            _bookList = GenerateBooksToTest();
            _target.BookList = _bookList;
        }

        [Test]
        public void TestGetBook()
        {
            var result = _target.GetBookById(_bookList[0].BookId);
            result.ShouldDeepEqual(_bookList[0]);
        }

        [Test]
        public void TestGetBookIgnoreProperty()
        {
            var originalBook = _bookList[0];

            var slightlyDifferentBook = new Book
            {
                AuthorId = originalBook.AuthorId,
                Title = "A new title",
                Type = originalBook.Type,
                Isbn = originalBook.Isbn,
                BookId = originalBook.BookId
            };

            var result = _target.GetBookById(originalBook.BookId);

            result.WithDeepEqual(slightlyDifferentBook)
                .IgnoreProperty<Book>(x => x.Title).Assert();
        }

        [Test]
        public void TestGetBookIgnoreUnmatchedProperty()
        {
            var originalBook = _bookList[0];

            var slightlyDifferentBook = new Book
            {
                AuthorId = originalBook.AuthorId,
                Title = "A new title",
                Type = originalBook.Type,
                Isbn = originalBook.Isbn,
                BookId = originalBook.BookId
            };

            var result = _target.GetBookById(originalBook.BookId);

            result.WithDeepEqual(slightlyDifferentBook)
                .IgnoreUnmatchedProperties().Assert();
            result.ShouldDeepEqual(originalBook);
        }

        [Test]
        public void TestGetBook3()
        {
            var originalBook = _bookList[0];

            var slightlyDifferentBook = new Book
            {
                AuthorId = originalBook.AuthorId,
                Title = "A new title",
                Type = originalBook.Type,
                Isbn = originalBook.Isbn,
                BookId = originalBook.BookId
            };

            var result = _target.GetBookById(originalBook.BookId);
            result.WithDeepEqual(slightlyDifferentBook)
                .IgnoreProperty<Book>(x => x.Title)
                .Assert();
        }

        private List<Book> GenerateBooksToTest()
        {
           return  _fixture.Build<Book>().CreateMany(10).ToList();
        }
    }
}
