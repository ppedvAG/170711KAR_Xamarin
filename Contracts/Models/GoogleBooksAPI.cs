﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Contracts.Models
{
    public class BookQuery
    {
        [JsonProperty("totalItems")]
        public int Count { get; set; }
        [JsonProperty("items")]
        public Book[] Books { get; set; }
    }

    public class Book
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("volumeInfo")]
        public BookInfo Info { get; set; }
    }

    public class BookInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("authors")]
        private string[] authors;
        public string Authors
        {
            get
            {
                //if (authors != null)
                //    return string.Join(", ", authors);
                //else
                //    return "---keine authoren---";

                return authors != null ? string.Join(", ", authors) : "--- Keine Autoren---";
                ////--------- Bedingung ---- True --------------------------- False
            }
        }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("publishedDate")]
        public string PublishedDate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("industryIdentifiers")]
        private Industryidentifier[] industryIdentifiers;
        public string IndustryIdentifiers
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                
                foreach(Industryidentifier identifier in industryIdentifiers)
                {
                    sb.AppendLine($"{identifier.Type}: {identifier.Identifier}");
                }
                return sb.ToString();
            }
        }
        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("categories")]
        private string[] categories;
        public string Categories => categories != null ? string.Join(", ", categories) : "--- Keine Kategorien ---";

        //public int Addieren(int zahl1, int zahl2)
        //{
        //    return zahl1 + zahl2;
        //}
        //public int Subtrahieren(int zahl1, int zahl2) => zahl1 - zahl2;

        [JsonProperty("maturityRating")]
        public string MaturityRating { get; set; }
        [JsonProperty("contentVersion")]
        public string ContentVersion { get; set; }
        [JsonProperty("imageLinks")]
        public Imagelinks ImageLinks { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("infoLink")]
        public string InfoLink { get; set; }
    }

    public class Imagelinks
    {
        [JsonProperty("smallThumbnail")]
        public string SmallThumbnail { get; set; }
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public class Industryidentifier
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }
}
