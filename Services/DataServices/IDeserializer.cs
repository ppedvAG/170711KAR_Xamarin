using BooksApp.Contracts.Models;

namespace BooksApp.Services.DataServices
{
    public interface IDeserializer
    {
        BookQuery DeserializeString(string json);
    }
}