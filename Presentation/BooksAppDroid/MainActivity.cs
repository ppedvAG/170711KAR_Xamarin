using Android.App;
using Android.Widget;
using Android.OS;
using BooksApp.Contracts.Models;
using System.Linq;

namespace BooksAppDroid
{
    [Activity(Label = "BooksAppDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button buttonSearch;
        private EditText editTextSearchText;
        private ListView listViewBooks;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            buttonSearch = FindViewById<Button>(Resource.Id.buttonSearch);
            editTextSearchText = FindViewById<EditText>(Resource.Id.editTextSearchText);
            listViewBooks = FindViewById<ListView>(Resource.Id.listViewBooks);

            buttonSearch.Click += ButtonSearch_Click;
        }

        private async void ButtonSearch_Click(object sender, System.EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(editTextSearchText.Text))
            {
                Toast.MakeText(this, "Bitte geben Sie einen Suchbegriff ein !", ToastLength.Long).Show();
                return;
            }

            ProgressDialog dlg = ProgressDialog.Show(this, "Loading", "Suche nach Bücher ...");
            BookQuery result = await ServiceProvider.BookService.FindBooksAsync(editTextSearchText.Text);

            if (result.Count == 0)
            {
                dlg.Dismiss();
                Toast.MakeText(this, "Es wurden leider keine Bücher gefunden !", ToastLength.Long).Show();
                return;
            }

            //LINQ
            //string[] titelArray = result.Books.Select(x => x.Info.Title).ToArray();
            //listViewBooks.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, titelArray);

            listViewBooks.Adapter = new BookAdapter(this, result.Books);

            dlg.Dismiss();
        }
    }
}

