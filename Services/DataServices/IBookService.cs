using BooksApp.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Services.DataServices
{
    public interface IBookService
    {
        BookQuery FindBooks(string searchText);
        Task<BookQuery> FindBooksAsync(string searchText);
    }
}
