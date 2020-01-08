using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Common.ViewModels;
using LiteDb.Common.Entities;
using LiteDb.Common.Models;
using Xamarin.Droid.Adapters;

namespace Xamarin.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ItemsAdapter adapter;
        private FloatingActionButton addBtn;

        private CarLiteDbDataStore model;
        private ItemsViewPresenter<Car> viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            model = new CarLiteDbDataStore();
            viewModel = new ItemsViewPresenter<Car>(model);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new ItemsAdapter(this, viewModel));

            addBtn = FindViewById<FloatingActionButton>(Resource.Id.fab);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);

            addBtn.Click += AddBtn_Click;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            addBtn.Click -= AddBtn_Click;

            model.Dispose();
            addBtn.Dispose();
            adapter.Dispose();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_delete_values)
            {
                viewModel.DeleteAllCommand.Execute(null);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void AddBtn_Click(object sender, EventArgs eventArgs)
        {
            var item = new Car() { Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            viewModel.AddItemCommand.Execute(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

