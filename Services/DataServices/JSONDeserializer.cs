using BooksApp.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Services.DataServices
{
    public class JSONDeserializer : IDeserializer
    {
        public BookQuery DeserializeString(string json)
        {
            return JsonConvert.DeserializeObject<BookQuery>(json);
        }
    }
}
