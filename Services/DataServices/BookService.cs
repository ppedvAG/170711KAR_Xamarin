using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksApp.Contracts.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace BooksApp.Services.DataServices
{
    public class BookService : IBookService
    {
        private readonly IBookDownloader downloader;
        private readonly IDeserializer deserializer;

        public BookService(IBookDownloader downloader, IDeserializer deserializer)
        {
            this.downloader = downloader;
            this.deserializer = deserializer;
        }

        public BookQuery FindBooks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException();

            string url = $"https://www.googleapis.com/books/v1/volumes?q={searchText}";
            string json;
            using (HttpClient client = new HttpClient())
            {
               json = client.GetStringAsync(url).Result; // Task wird sofort ausgeführt -> syncron anstelle von asyncron
            }
            return JsonConvert.DeserializeObject<BookQuery>(json);
        }

        public async Task<BookQuery> FindBooksAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException();

            string json = await downloader.DownloadStringAsync($"https://www.googleapis.com/books/v1/volumes?q={searchText}");
            return deserializer.DeserializeString(json);
        }
    }
}
