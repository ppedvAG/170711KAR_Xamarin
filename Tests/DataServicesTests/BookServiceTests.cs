using BooksApp.Contracts.Models;
using BooksApp.Services.DataServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Tests.DataServicesTests
{
    [TestFixture]
    class BookServiceTests
    {
        private IBookService service;

        [OneTimeSetUp] // Initialisierung, bevor alle Tests durchgeführt werden
        public void Init()
        {
            service = new BookService(new JSONBookDownloader(), new JSONDeserializer());
        }

        #region FindBooksTests
        [Test]
        [TestCase("xamarin")]
        [TestCase("wpf")]
        public void FindBooksTest_getValidBookQueryWithValidInput(string searchText)
        {
            BookQuery result = service.FindBooks(searchText);
            Assert.NotNull(result);
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        public void FindBooksTest_getExceptionWhenInputIsNotValid(string searchText)
        {
            TestDelegate del = new TestDelegate(() =>
           {
               BookQuery result = service.FindBooks(searchText);
           });
            Assert.Throws<ArgumentException>(del);
        }

        [Test]
        [TestCase("einbuchdasdefinitivnichtexistiert")]
        public void FindBooksTest_ResultCountIsZeroWhenNoBooksFound(string searchText)
        {
            BookQuery result = service.FindBooks(searchText);
            Assert.NotNull(result);
            Assert.Zero(result.Count);
        }
        #endregion

        #region FindBooksAsyncTests

        [Test]
        [TestCase("xamarin")]
        [TestCase("wpf")]
        public void FindBooksAsyncTest_getValidBookQueryWithValidInput(string searchText)
        {
            BookQuery result = service.FindBooksAsync(searchText).Result;
            Assert.NotNull(result);
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        public void FindBooksAsyncTest_getExceptionWhenInputIsNotValid(string searchText)
        {
            AsyncTestDelegate del = new AsyncTestDelegate(async() =>
            {
                BookQuery result = await service.FindBooksAsync(searchText);
            });
            Assert.ThrowsAsync<ArgumentException>(del);
        }

        [Test]
        [TestCase("einbuchdasdefinitivnichtexistiert")]
        public void FindBooksAsyncTest_ResultCountIsZeroWhenNoBooksFound(string searchText)
        {
            BookQuery result = service.FindBooksAsync(searchText).Result;
            Assert.NotNull(result);
            Assert.Zero(result.Count);
        }

        #endregion

    }
}
