using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BooksApp.Services.DataServices;

namespace BooksAppDroid
{
    public static class ServiceProvider
    {
        static ServiceProvider()
        {
            Deserializer = new JSONDeserializer();
            Downloader = new JSONBookDownloader();
            BookService = new BookService(Downloader, Deserializer);
        }

        public static IBookService BookService { get; set; }
        public static IDeserializer Deserializer { get; set; }
        public static IBookDownloader Downloader { get; set; }

        // Alternative
        //private static IDeserializer deserializer;
        //public static IDeserializer Deserializer_2
        //{
        //    get
        //    {
        //        deserializer = deserializer ?? new JSONDeserializer();
        //        return deserializer;
        //    }
        //}
    }
}