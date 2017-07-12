
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
using BooksApp.Contracts.Models;
using System.Net;
using Android.Graphics;
using System.Threading.Tasks;

namespace BooksAppDroid
{
    public class BookAdapter : BaseAdapter<Book>
    {
        private Activity context;
        private Book[] items;
        private Dictionary<string, Bitmap> imageCache;

        public BookAdapter(Activity context, Book[] items)
        {
            this.context = context;
            this.items = items;
            imageCache = new Dictionary<string, Bitmap>();
        }

        public override Book this[int position] => items[position];

        public override int Count => items.Length;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.BookItemLayout, parent, false);

            TextView textViewTitle = v.FindViewById<TextView>(Resource.Id.bookItemTextViewTitle);
            TextView textViewSubtitle = v.FindViewById<TextView>(Resource.Id.bookItemTextViewSubtitle);
            ImageView imageViewThumbnail = v.FindViewById<ImageView>(Resource.Id.bookItemImageViewThumbnail);

            textViewTitle.Text = this[position].Info.Title;
            textViewSubtitle.Text = this[position].Info.Subtitle;


            if (this[position]?.Info?.ImageLinks?.SmallThumbnail != null)
            {

                if (imageCache.ContainsKey(this[position].Info.ImageLinks.SmallThumbnail) == false)
                { // Bild ist noch nicht vorhanden -> herunterladen
                    Task.Run(() =>
                    {
                        using (WebClient client = new WebClient())
                        {
                            byte[] data = client.DownloadData(this[position].Info.ImageLinks.SmallThumbnail);
                            imageCache.Add(this[position].Id, BitmapFactory.DecodeByteArray(data, 0, data.Length));
                            imageViewThumbnail.SetImageBitmap(imageCache[this[position].Id]);
                        }
                    });
                }
                else
                    imageViewThumbnail.SetImageBitmap(imageCache[this[position].Info.ImageLinks.SmallThumbnail]);
            }
            else // Kein Bild vorhanden -> Platzhalterbild verwenden
            {
                imageViewThumbnail.SetImageResource(Resource.Drawable.Icon);
            }


            return v;
        }
    }
}